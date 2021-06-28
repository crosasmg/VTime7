Option Strict Off
Option Explicit On
Friend Class VisualTimeConfig
	
	Private mstrConfigContent As String
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Dim strDrive As String
		
		strDrive = My.Application.Info.DirectoryPath
		If strDrive > String.Empty Then
			strDrive = Left(strDrive, 2)
		Else
			strDrive = "D:"
		End If
		
        mstrConfigContent = eRemoteDB.FileSupport.LoadFileToText(strDrive & "\VisualTIMENet\Configuration\VisualTIMEConfig.xml")
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub

    'UPGRADE_NOTE: Default was upgraded to Default_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Sub LoadSetting(ByVal sKey As String, Optional ByRef Default_Renamed As Object = Nothing, Optional ByVal sGroup As String = "Settings")
        Dim lstrGroup As String
        Dim varAux As String = ""


        lstrGroup = GetBlock(mstrConfigContent, sGroup, True)
        If lstrGroup <> String.Empty Then
            varAux = GetBlock(lstrGroup, sKey, True)
        End If
    End Sub

    Private Function GetBlock(ByRef sSource As String, ByVal sTag As String, Optional ByRef bNotDelete As Boolean = False) As String
        Dim strLabel As String
        Dim lngIniPosition As Integer
        Dim lngEndPosition As Integer


        strLabel = "<" & UCase(sTag) & ">"
        lngIniPosition = InStr(UCase(sSource), strLabel)
        If lngIniPosition > 0 Then
            lngIniPosition = lngIniPosition + Len(strLabel)
            strLabel = "</" & UCase(sTag) & ">"
            lngEndPosition = InStr(lngIniPosition, UCase(sSource), strLabel)
            If lngEndPosition > 0 Then
                GetBlock = Mid(sSource, lngIniPosition, lngEndPosition - lngIniPosition)
                If Not bNotDelete Then
                    sSource = Left(sSource, lngIniPosition + 1) & Mid(sSource, lngEndPosition)
                End If
            End If
        End If
    End Function
End Class






