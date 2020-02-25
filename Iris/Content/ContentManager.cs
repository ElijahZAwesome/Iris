using Iris.Content.ContentImporters;
using Iris.Exceptions;
using Iris.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Iris.Content
{
    public class ContentManager
    {
        internal Dictionary<Type, ContentImporter> Importers { get; }

        public string ContentRoot { get; }

        public ContentManager(string contentRoot)
        {
            ContentRoot = contentRoot;
            Importers = new Dictionary<Type, ContentImporter>();

            RegisterBuiltinImporters();
        }

        internal ContentManager()
            : this(
                 Path.Combine(
                     Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                     "Content"
                 )
             )
        { }

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

        public void RegisterImporter<T, U>() where U : ContentImporter<T>
        {
            if (Importers.ContainsKey(typeof(U)))
                return; // TODO: Throw/log something in future?

            Importers.Add(typeof(T), Activator.CreateInstance<U>());
        }

        internal void RegisterBuiltinImporters()
        {
            RegisterImporter<Sprite, SpriteImporter>();
            RegisterImporter<PixelShader, PixelShaderImporter>();
        }
    }
}
