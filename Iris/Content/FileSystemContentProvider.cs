using Iris.Content.ContentImporters;
using Iris.Exceptions;
using Iris.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Iris.Content
{
    public class FileSystemContentProvider : IContentProvider
    {
        public FileSystemContentProvider(Dictionary<Type, ContentImporter> importers, string contentRoot)
        {
            Importers = importers;
            ContentRoot = contentRoot;
        }

        internal Dictionary<Type, ContentImporter> Importers { get; }

        public string ContentRoot { get; }

        public FileSystemContentProvider(string contentRoot)
        {
            ContentRoot = contentRoot;
            Importers = new Dictionary<Type, ContentImporter>();

            RegisterBuiltinImporters();
        }

        internal FileSystemContentProvider()
            : this(
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    "Content"
                )
            )
        {
        }

        public Stream GetRawContentFileStream(string path, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read)
        {
            return new FileStream(
                Path.Combine(ContentRoot, path),
                fileMode,
                fileAccess
            );
        }

        public string GetRawContentFileString(string path)
        {
            using var sr = new StreamReader(GetRawContentFileStream(path));
            return sr.ReadToEnd();
        }

        public T Load<T>(string relativePath) where T : class
        {
            var completePath = Path.Combine(ContentRoot, relativePath);

            if (!File.Exists(completePath))
                throw new ContentPathException(completePath, "Could not find a file at the provided path.");

            var type = typeof(T);
            if (!Importers.ContainsKey(type))
                throw new ContentUnsupportedException(type, "The requested type has no importers registered.");

            return Importers[type].ImportObject(completePath) as T;
        }

        public byte[] Read(string relativePath)
        {
            var completePath = Path.Combine(ContentRoot, relativePath);

            if (!File.Exists(completePath))
                throw new ContentPathException(completePath, "Could not find a file at the provided path.");

            return File.ReadAllBytes(completePath);
        }

        public Stream GetFileStream(string relativePath)
            => new FileStream(Path.Combine(ContentRoot, relativePath), FileMode.Open);
        
        public async Task<T> LoadAsync<T>(string relativePath) where T : class
            => await Task.Run(() => Load<T>(relativePath));

        public async Task<byte[]> ReadAsync(string relativePath)
            => await Task.Run(() => Read(relativePath));

        public void RegisterImporter<T, U>() where U : ContentImporter<T>
        {
            if (Importers.ContainsKey(typeof(U)))
                return; // TODO: Throw/log something in future?

            Importers.Add(typeof(T), Activator.CreateInstance<U>());
        }

        internal void RegisterBuiltinImporters()
        {
            RegisterImporter<Sprite, SpriteImporter>();
            RegisterImporter<Spritesheet, SpritesheetImporter>();
            RegisterImporter<PixelShader, PixelShaderImporter>();
            RegisterImporter<Font, FontImporter>();
        }
    }
}