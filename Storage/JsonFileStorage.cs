using LibGit2Sharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Storage
{
    public class JsonFileStorage : IStorage
    {
        private readonly bool _enableVersionControl;
        private readonly IRepository _repository = null;
        private readonly string _storageFolder;

        private static readonly object lockObj = new object();

        public JsonFileStorage(string storageFolder, bool enableVersionControl)
        {
            _storageFolder = storageFolder;
            Directory.CreateDirectory(_storageFolder);

            _enableVersionControl = enableVersionControl;
            if (_enableVersionControl)
            {
                if (!Repository.IsValid(storageFolder))
                {
                    Repository.Init(storageFolder);
                }

                _repository = new Repository(storageFolder);
            }
        }

        public async Task<T> Create<T>(T obj, bool overwriteIfExists = false)
        {
            var (id, fullFileName) = GetIdAndFileName(obj);

            if (!overwriteIfExists && File.Exists(fullFileName))
            {
                throw new StorageException(new LogEvent
                {
                    Message = $"Object {typeof(T).FullName} with Id = {id} already exists.",
                    Data = new Dictionary<string, object>
                    {
                        { id, obj }
                    }
                });
            }

            // Write the data
            var content = JsonConvert.SerializeObject(obj, Formatting.Indented);
            await File.WriteAllTextAsync(fullFileName, content);

            ExecuteVersionControl($"Creating {typeof(T).FullName} '{obj.GetValue<string>("Id")}'", fullFileName);

            return obj;
        }

        private async Task<T> CreateObjectFromFile<T>(string fullFileName)
        {
            var content = await File.ReadAllTextAsync(fullFileName);
            return JsonConvert.DeserializeObject<T>(content);
        }

        public Task<T> Read<T>(string id)
        {
            return CreateObjectFromFile<T>(
                GetFullFileName<T>(id)
            );
        }

        public async Task<IEnumerable<T>> ReadBy<T>(Func<T, bool> predicate)
        {
            var folder = GetStorageFolderForType<T>();

            var files = Directory.GetFiles(folder);
            var tasks = files.Select(s => CreateObjectFromFile<T>(s));
            var objs = await Task.WhenAll(tasks);

            return objs.Where(predicate);
        }

        public async Task<T> Update<T>(T obj)
        {
            var sourceId = obj.GetValue<string>("Id");

            var target = await Read<T>(sourceId);
            if (target == null)
            {
                throw new StorageException(new LogEvent
                {
                    Message = $"Update {typeof(T).FullName} fail because Id = {sourceId} does not exists.",
                    Data = new Dictionary<string, object>
                    {
                        {"Id", sourceId}
                    }
                });
            }

            return await Create(obj, true);
        }

        public Task<bool> Delete<T>(string id)
        {
            var fullFileName = GetFullFileName<T>(id);

            if (File.Exists(fullFileName))
            {
                File.Delete(fullFileName);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Exists<T>(string id)
        {
            return Task.FromResult(
                File.Exists(
                    GetFullFileName<T>(id)
                )
            );
        }

        private string GetStorageFolderForType<T>()
        {
            // Prepares the folder, using the type name...
            var folder = Path.Combine(
                            _storageFolder,
                            typeof(T).FullName
                        );

            Directory.CreateDirectory(folder);

            return folder;
        }

        private string GetFullFileName<T>(string id)
        {
            var folder = GetStorageFolderForType<T>();

            var fullFileName = Path.Combine(
                                    folder,
                                    id.RemoveInvalidFileNameChars()
                                    );

            return fullFileName;
        }

        private (string Id, string FullFileName) GetIdAndFileName<T>(T obj)
        {
            // Create the full file name
            if (obj.TryGetValue<string>("Id", out var id))
            {
                return (id, GetFullFileName<T>(id));
            }

            throw new StorageException(new LogEvent
            {
                Message = $"Id property was not found for type {typeof(T).FullName}",
                Data = new Dictionary<string, object>
                {
                    { "T", obj }
                }
            });
        }

        private void ExecuteVersionControl(string message, string fileName)
        {
            if (_enableVersionControl)
            {
                lock (lockObj)
                {
                    var gitFileName = fileName.Replace(_repository.Info.WorkingDirectory, "");
                    _repository.Index.Add(gitFileName);
                    _repository.Index.Write();
                    try
                    {
                        _repository.Commit(
                            message,
                            new Signature(this.GetType().FullName, "author@email", new DateTimeOffset()),
                            new Signature(this.GetType().FullName, "commiter@email", new DateTimeOffset())
                        );
                    }
                    catch (LibGit2Sharp.EmptyCommitException) { }
                }
            }
        }
    }
}