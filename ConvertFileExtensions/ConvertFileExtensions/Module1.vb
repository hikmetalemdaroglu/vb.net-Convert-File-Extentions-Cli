' Usage : ConvertFileExtensions.exe "C:\temp" .fiif .jpg "C:\temp\convert.log"
' Prompt : 
'
' command line vb.net netframework ile yazılacak bir uygulamaya ihtiyacım var.
' bir directory deki bütün dosyaların soneklerini başka bir son eke çevirmeli.
' örneğin .fiif uzantılı dosyaların son eklerini .jpg ye çevirmeli.
' Giriş parametreleri dizinin full path'i (örneğin c:\temp )
' değişmesi istenen ilk sonek ( örneğin .fiif) hedef sonek ( örneğin : .jpg).
' bir log dosyası oluşturularak her bir işlemin durumu csv formatında yazılmalı.
' Log dosyası kolunları şöyle olmalı. full path dosya ismi, statu ( Basarili/hatali),
' Açıklama ( hata varsa bu alana yazılacak). csv ayraci  noktalı virgül olacak (;).
' log dosyasının ismi son parametre olarak verilecek. (örneğin c:\temp\convert.log)

Imports System.IO

Module Module1

    Sub Main(args As String())
        If args.Length <> 4 Then
            Console.WriteLine("Kullanım: ConvertFileExtensions <dizin> <ilk_sonek> <hedef_sonek> <log_dosyası>")
            Return
        End If

        Dim directoryPath As String = args(0)
        Dim initialExtension As String = args(1)
        Dim targetExtension As String = args(2)
        Dim logFilePath As String = args(3)

        ' Çıkış dizini yoksa oluştur
        If Not Directory.Exists(directoryPath) Then
            Directory.CreateDirectory(directoryPath)
        End If


        Try
            Dim files As String() = Directory.GetFiles(directoryPath, $"*{initialExtension}")
            Using logWriter As New StreamWriter(logFilePath, False)
                For Each filePath As String In files
                    Try
                        Dim newFilePath As String = Path.ChangeExtension(filePath, targetExtension)
                        File.Move(filePath, newFilePath)
                        logWriter.WriteLine($"{filePath};Basarili;")
                    Catch ex As Exception
                        logWriter.WriteLine($"{filePath};Hatali;{ex.Message}")
                    End Try
                Next
            End Using
            Console.WriteLine("Dönüşüm tamamlandı. Log dosyası: " & logFilePath)
        Catch ex As Exception
            Console.WriteLine("Bir hata oluştu: " & ex.Message)
        End Try
    End Sub

End Module

