﻿using System;

namespace TerminalGame.Files.FileSystem
{
    public class FileSystem
    {
        public File RootDir { get; set; }
        public File CurrentDir { get; set; }
        public File LastDir { get; set; }

        public FileSystem()
        {

        }

        public FileSystem(File root)
        {
            if (root.FileType is FileType.Directory)
            {
                RootDir = root;
                CurrentDir = RootDir;
            }
            else throw new ArgumentException("Root file has to be a directory.");
        }

        public bool TryFindFile(string name, out File result)
        {
            if (name == ".")
            {
                result = CurrentDir;
                return true;
            }
            if (name == "..")
            {
                if (CurrentDir.Parent == null) // we're at root (/), nowhere else to go
                    result = CurrentDir;
                else
                    result = CurrentDir.Parent;
                return true;
            }
            if (name == "/")
            {
                result = RootDir;
                return true;
            }

            result = CurrentDir.Children.Find(f => f.Name == name);
            return result != null;
        }

        public bool TryFindFileFromPath(string filePath, out string path, out File file)
        {
            path = "";
            file = null;
            var origin = CurrentDir;
            var split = filePath.Split('/');
            var destdir = split.Length - 1;
            var potentialPath = "/" + string.Join('/', split[0..destdir]) + "/";
            var potentialFile = split[destdir];

            if (TryFindFilePath(potentialFile, out string actualPath))
            {
                if (actualPath == potentialPath)
                {
                    path = potentialPath;
                    ChangeCurrentDirFromPath(path);
                    if (TryFindFile(potentialFile, out File f))
                    {
                        file = f;
                        CurrentDir = origin;
                        return true;
                    }
                }
            }
            CurrentDir = origin;
            return false;
        }

        public bool TryFindFilePath(string name, out string path)
        {
            bool getPath(File file, out string pPath)
            {
                pPath = "";
                if (file.Name == name)
                {
                    return true;
                }
                if (file.FileType == FileType.Directory)
                {
                    foreach (var f in file.Children)
                    {
                        if (getPath(f, out string tempPath))
                        {
                            pPath += file.Name + "/" + tempPath;
                            return true;
                        }
                    }
                }
                return false;
            }
            if (getPath(RootDir, out string outPath))
            {
                path = outPath;
                return true;
            }
            path = "";
            return false;
        }

        public void ChangeCurrentDirFromPath(string path)
        {
            var splitPath = path.Split('/');
            var currentDir = CurrentDir;
            var lastDir = LastDir;

            foreach (var dir in splitPath)
            {
                if (TryFindFile(dir, out File directory))
                {
                    if (directory.FileType == FileType.Directory)
                    {
                        ChangeCurrentDir(directory);
                    }
                    else
                    {
                        CurrentDir = lastDir;
                        throw new ArgumentException("Invalid path.");
                    }
                }
            }
            LastDir = currentDir;
        }

        public void ChangeCurrentDir(File directory)
        {
            if (directory == null)
                return;
            if (directory.FileType == FileType.Directory)
            {
                LastDir = CurrentDir;
                CurrentDir = directory;
            }
            else throw new ArgumentException($"{directory} is not a directory.");
        }

        public override string ToString()
        {
            // TODO: something...?
            return base.ToString();
        }
    }
}
