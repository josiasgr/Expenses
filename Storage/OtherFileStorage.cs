﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Storage
{
    public class OtherFileStorageConfig
    {
        public string Entity { get; set; }
        public string Table { get; set; }
    }

    public class OtherFileStorage : IStorage
    {
        private readonly string _storageFolder;

        public OtherFileStorage(OtherFileStorageConfig otherFileStorageConfig)
        {
            _storageFolder = otherFileStorageConfig.Table;
            Directory.CreateDirectory(_storageFolder);
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
    }
}