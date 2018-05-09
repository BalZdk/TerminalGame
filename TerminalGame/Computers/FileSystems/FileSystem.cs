﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalGame.Computers.FileSystems
{
    class FileSystem
    {
        public List<File> Children;
        public File CurrentDir { get; private set; }

        public FileSystem()
        {
            File root = new File(File.FileType.Directory, "/");
            root.SetParent(root);
            CurrentDir = root;
            Children = new List<File>() { root };
        }

        public File FindDir(string name)
        {
            if (name == "..")
                return CurrentDir.Parent;

            bool findFile(File f)
            { return f.Name == name && f.IsDirectory; }
            return CurrentDir.Children.Find(findFile);
        }

        public File FindFile(string name)
        {
            if (name == "..")
                return CurrentDir.Parent;

            bool findFile(File f)
            { return f.Name == name; }
            return CurrentDir.Children.Find(findFile);
        }

        public void ChangeDir(string name)
        {
            if (name == "..")
            {
                CurrentDir = CurrentDir.Parent;
            }
            else if (FindFile(name) != null)
            {
                bool findFile(File f)
                { return f.Name == name && f.IsDirectory && !f.Equals(CurrentDir); }
                CurrentDir = CurrentDir.Children.Find(findFile);
            }
        }

        public void AddFile(string name)
        {
            File f = new File(File.FileType.File, name);
            f.SetParent(CurrentDir);
            CurrentDir.Children.Add(f);
        }

        public void RemoveFile(File file)
        {
            if (CurrentDir.Children.Remove(file))
            {
                // if file is removed
            }
            else
            {

            }
        }

        public void AddDir(string name)
        {
            File f = new File(File.FileType.Directory, name);
            f.SetParent(CurrentDir);
            CurrentDir.Children.Add(f);
        }

        public string ListFiles()
        {
            return ListFiles(CurrentDir);
        }

        private string ListFiles(File file)
        {
            string retval = "";
            if (file.Parent != CurrentDir)
                retval += "<DIR> ..\n§";
            foreach (File f in file.Children)
            {
                if (f.IsDirectory)
                    retval += "<DIR> " + f.Name + "\n§";
                else
                    retval += f.Name + "\n§";
            }
            return retval;
        }
    }
}
