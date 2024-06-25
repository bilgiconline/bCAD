Imports System.IO

Module FileManagement
    Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal process As IntPtr, ByVal minimumWorkingSetSize As Integer, ByVal maximumWorkingSetSize As Integer) As Integer

    Function FileorFolderExists(Path As String) As Boolean
        Try
            If Directory.Exists(Path) = True Then
                FileorFolderExists = True
                Exit Function
            End If
            If File.Exists(Path) = True Then
                FileorFolderExists = True
            End If
        Catch ex As Exception
            FileorFolderExists = False
        End Try
        Return FileorFolderExists
    End Function

    Function GetFileName(FileFullPath As String, WithExtension As Boolean) As String
        If InStr(FileFullPath, "\") > 0 And Not InStr(FileFullPath, "/") > 0 Then
            GetFileName = Mid(FileFullPath, InStrRev(FileFullPath, "\") + 1)
        ElseIf InStr(FileFullPath, "/") > 0 And Not InStr(FileFullPath, "\") > 0 Then
            GetFileName = Mid(FileFullPath, InStrRev(FileFullPath, "/") + 1)
        Else
            GetFileName = "File path not correct."
            Exit Function
        End If
        If WithExtension = False Then
            GetFileName = Mid(GetFileName, 1, InStrRev(GetFileName, ".") - 1)
        End If
        Return GetFileName
    End Function
    Function GetFilePath(FileFullPath As String) As String
        If InStr(FileFullPath, "\") > 0 And Not InStr(FileFullPath, "/") > 0 Then
            GetFilePath = Mid(FileFullPath, 1, InStrRev(FileFullPath, "\"))
        ElseIf InStr(FileFullPath, "/") > 0 And Not InStr(FileFullPath, "\") > 0 Then
            GetFilePath = Mid(FileFullPath, 1, InStrRev(FileFullPath, "/"))
        Else
            GetFilePath = "File path not correct."
        End If
        Return GetFilePath
    End Function
    Function StreamReaderProgram(Path As String) As String
        Dim Start As Boolean = False
        Try
            If File.Exists(Path) = True Then
                Dim ReadStream As Stream = New FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite) 'New Stream(Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Using StreamReaderGetInfos As StreamReader = New StreamReader(ReadStream, System.Text.Encoding.GetEncoding("Windows-1254")) 'FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    While Not StreamReaderGetInfos.EndOfStream
                        Dim aReadLine As String
                        aReadLine = StreamReaderGetInfos.ReadLine
                        If Start = False Then
                            StreamReaderProgram = ToolboxConvertTurkishCh(aReadLine)
                            Start = True
                        Else
                            StreamReaderProgram = StreamReaderProgram & vbCrLf & ToolboxConvertTurkishCh(aReadLine)
                        End If
                    End While
                    StreamReaderGetInfos.Close()
                    StreamReaderGetInfos.Dispose()
                End Using
                '  StreamReaderProgram = StreamReaderProgram.Replace(Chr(10), "")
                If StreamReaderProgram = "" Then
                    StreamReaderProgram = ""
                End If
            Else
                StreamReaderProgram = ""
            End If
        Catch ex As Exception
            StreamReaderProgram = ""
        End Try
        Return StreamReaderProgram
    End Function
    Function StreamWriterProgram(Path As String, StringofLine As String) As String
        CheckDirectoriesandCreate(Path)
        If File.Exists(Path) = False Then
            File.Create(Path).Close()
            File.CreateText(Path).Dispose()
        End If
        Dim enc As Text.Encoding
        enc = System.Text.Encoding.GetEncoding("Windows-1254")
        Try
            Using WriterStream As StreamWriter = New StreamWriter(Path, True, System.Text.Encoding.GetEncoding("Windows-1254"))
                WriterStream.WriteLine(ToolboxConvertTurkishCh(StringofLine.ToString))
                WriterStream.Close()
                WriterStream.Dispose()
            End Using
        Catch ex As Exception
            KillProcess(GetFileName(Path, True))
            Using WriterStream As New StreamWriter(Path, True, System.Text.Encoding.GetEncoding("Windows-1254"))
                WriterStream.WriteLine(ToolboxConvertTurkishCh(StringofLine.ToString))
                WriterStream.Close()
                WriterStream.Dispose()
            End Using
        End Try
        Return StreamWriterProgram
    End Function
    Sub CheckDirectoriesandCreate(PathDir As String)
        Dim i
        Dim tmpStringList() As String
        Dim LocalorConnectionPath As String
        Dim tmpStr() As String
        tmpStr = PathDir.Split("\")
        If Mid(PathDir, 1, 2) = "\\" Then
            LocalorConnectionPath = "\\" & tmpStr(2) & "\"
        ElseIf Mid(PathDir, 2, 2) = ":\" Then
            LocalorConnectionPath = tmpStr(0) & "\"
        Else
            Exit Sub
        End If
        If PathDir.EndsWith("\") Then PathDir = Mid(PathDir, 1, InStrRev(PathDir, "\") - 1)
        tmpStringList = PathDir.Replace(LocalorConnectionPath, "").Split("\")
        Dim tmpCumulativestring As String
        For i = 0 To tmpStringList.Count - 1
            If InStr(tmpStringList(i).ToString, ".") = 0 Then
                If i = 0 Then
                    tmpCumulativestring = LocalorConnectionPath & tmpStringList(i).ToString
                    If Directory.Exists(tmpCumulativestring) = False Then
                        My.Computer.FileSystem.CreateDirectory(tmpCumulativestring)
                    End If
                Else
                    tmpCumulativestring = tmpCumulativestring & "\" & tmpStringList(i).ToString
                    If Directory.Exists(tmpCumulativestring) = False Then
                        My.Computer.FileSystem.CreateDirectory(tmpCumulativestring)
                    End If
                End If
            End If
        Next
    End Sub
    Function ToolboxConvertTurkishCh(originalWord As String) As String
        ToolboxConvertTurkishCh = originalWord
        If Not originalWord = "" Then
            ToolboxConvertTurkishCh = originalWord
            ToolboxConvertTurkishCh = ToolboxConvertTurkishCh.Replace("ï»¿", "")
            ToolboxConvertTurkishCh = ToolboxConvertTurkishCh.Replace("Ã‡", "Ç")
            ToolboxConvertTurkishCh = ToolboxConvertTurkishCh.Replace("Ã§", "ç")
            ToolboxConvertTurkishCh = ToolboxConvertTurkishCh.Replace("Ãœ", "Ü")
            ToolboxConvertTurkishCh = ToolboxConvertTurkishCh.Replace("Ã¼", "ü")
            ToolboxConvertTurkishCh = ToolboxConvertTurkishCh.Replace("Å", "Ş")
            ToolboxConvertTurkishCh = ToolboxConvertTurkishCh.Replace("ÅŸ", "ş")
            ToolboxConvertTurkishCh = ToolboxConvertTurkishCh.Replace("Ä°", "İ")
            ToolboxConvertTurkishCh = ToolboxConvertTurkishCh.Replace("Ä±", "ı")
            ToolboxConvertTurkishCh = ToolboxConvertTurkishCh.Replace("Ã–", "Ö")
            ToolboxConvertTurkishCh = ToolboxConvertTurkishCh.Replace("Ã¶", "ö")
            ToolboxConvertTurkishCh = ToolboxConvertTurkishCh.Replace("ï»¿", "")
            ToolboxConvertTurkishCh = ToolboxConvertTurkishCh.Replace("ÄŸ", "ğ")
            ToolboxConvertTurkishCh = ToolboxConvertTurkishCh.Replace("Ä", "Ğ")
        End If
        Return ToolboxConvertTurkishCh
    End Function
    Function KillProcess(InSTRWindowsTitle As String) As String
        KillProcess = "Process not found."
        For Each tmpProcess As Process In Process.GetProcesses
            If Not tmpProcess.ProcessName = "" And InStr(LCase(tmpProcess.MainWindowTitle), LCase((InSTRWindowsTitle))) > 0 Then
                tmpProcess.Kill()
                KillProcess = "Succesfull."
            End If
        Next
        Return KillProcess
    End Function
    Sub FlushMemory()
        Try
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Module
