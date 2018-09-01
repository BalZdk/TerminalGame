﻿using System;
using System.Collections.Generic;

namespace TerminalGame.Computers.FileSystems
{
    class FileSystem
    {
        public event EventHandler ChangeDirectory;
        public List<File> Children;
        public File CurrentDir { get; private set; }

        public FileSystem()
        {
            File root = new File("/");
            root.SetParent(root);
            CurrentDir = root;
            Children = new List<File>() { root };
        }

        public void BuildBasicFileSystem()
        {
            string[] baseDirs = { "bin", "usr", "home", "sys", "logs" };
            for (int i = 0; i < baseDirs.Length; i++)
            {
                AddDir(baseDirs[i]);
            }
        }

        private File FindRoot(File sourceDir)
        {
            return sourceDir.Parent.Parent != sourceDir.Parent ? FindRoot(sourceDir.Parent) : sourceDir.Parent;
        }

        public File FindFile(string name, bool isDir, bool fromRoot = false)
        {
            if (name == "..")
                return CurrentDir.Parent;

            if (name == "/")
                return FindRoot(CurrentDir);

            bool findFile(File f)
            {
                return isDir ? f.Name == name && f.IsDirectory : f.Name == name;
            }
            return fromRoot ? FindRoot(CurrentDir).Children.Find(findFile) : CurrentDir.Children.Find(findFile);
        }

        public bool TryFindFile(string name, bool isDir, bool fromRoot = false)
        {
            if (name == "..")
                return true;

            if (name == "/")
                return true;

            if (fromRoot)
            {
                foreach (File f in FindRoot(CurrentDir).Children)
                {
                    if (f.Name == name)
                    {
                        return isDir ? f.IsDirectory : !f.IsDirectory;
                    }
                }
            }
            else
            {
                foreach (File f in CurrentDir.Children)
                {
                    if (f.Name == name)
                    {
                        return isDir ? f.IsDirectory : !f.IsDirectory;
                    }
                }
            }
            return false;
        }

        public void ChangeDir(string name)
        {
            if (name == "..")
            {
                CurrentDir = CurrentDir.Parent;
            }
            else if (name == "/")
            {
                CurrentDir = FindRoot(CurrentDir);
            }
            else if (FindFile(name, true) != null)
            {
                bool findFile(File f)
                { return f.Name == name && f.IsDirectory && !f.Equals(CurrentDir); }
                CurrentDir = CurrentDir.Children.Find(findFile);
                ChangeDirectory?.Invoke(this, new EventArgs());
            }
        }

        public void AddFile(string name, string contents = null)
        {
            File f = new File(name, contents);
            f.SetParent(CurrentDir);
            CurrentDir.Children.Add(f);
        }

        // TODO: Add ability to take full paths as input, e.g. /bin/subfolder/file
        public void AddFileToDir(string directoryName, string name, string contents = null)
        {
            if (TryFindFile(directoryName, true))
            {
                File f = new File(name, contents);
                f.SetParent(FindFile(directoryName, true));
                FindFile(directoryName, true).Children.Add(f);
            }
            else if (TryFindFile(directoryName, true, true))
            {
                File f = new File(name, contents);
                f.SetParent(FindFile(directoryName, true, true));
                FindFile(directoryName, true, true).Children.Add(f);
            }
            //else
            //    throw new Exception(directoryName + " is not a directory.");
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
            File f = new File(name);
            f.SetParent(CurrentDir);
            CurrentDir.Children.Add(f);
        }

        public string ListFiles()
        {
            return ListFiles(CurrentDir);
        }

        private string ListFiles(File directory)
        {
            //Sorts alphabetically
            directory.Children.Sort();
            string retval = "";
            if (directory.Parent != CurrentDir)
            {
                retval += "\n    <DIR>    .§";
                retval += "\n    <DIR>    ..§";
            }
            
            foreach (File f in directory.Children)
            {
                if (f.IsDirectory)
                    retval += "\n    <DIR>    " + f.Name + "§";
                else
                    retval += "\n             " + f.Name + "§";
            }
            return retval;
        }
    }
}
