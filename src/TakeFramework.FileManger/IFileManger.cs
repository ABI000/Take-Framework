/*
 * <Your-Product-Name>
 * Copyright (c) <Year-From>-<Year-To> <Your-Company-Name>
 *
 * Please configure this header in your SonarCloud/SonarQube quality profile.
 * You can also set it in SonarLint.xml additional file for SonarLint or standalone NuGet analyzer.
 */

using System.Collections.ObjectModel;

namespace TakeFramework.FileManger;

public interface IFileManger
{

    /// <summary>
    /// Copy File/Folders
    /// </summary>
    public void Copy(string targetPath, Collection<string> folders);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetPath"></param>
    /// <param name="folders"></param>
    public void Move(string targetPath, Collection<string> folders);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="folders"></param>
    public void Delete(Collection<string> folders);
    /// <summary>
    /// 
    /// </summary>
    public void Create(Stream fileStream, string targetPath, string fileName);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="folders"></param>
    public void Remove(Collection<string> folders);
}
