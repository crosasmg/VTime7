Option Strict Off
Option Explicit On

Friend Class Message
	
    Public nErrorType As Errors.ErrorsType
	
	Private mblnChange As Boolean
	Private mstrCodispl As String
	Private mstrMessage As String
	Private mstrCachePath As String
	Private mblnCacheEnabled As Boolean
    Private mCurrentCulture As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture
    Private mintCompany As integer
	
    Public Sub New()
        MyBase.New()
        Dim clsConfig As New eRemoteDB.VisualTimeConfig
        Dim objContext As New eRemoteDB.ASPSupport
        
        mstrCachePath = clsConfig.LoadSetting("Cache", "C:\VisualTIMENet\VTimeNet\Cache", "Paths")
        mblnCacheEnabled = (UCase(clsConfig.LoadSetting("CacheEnabled", "Yes", "Database")) = "YES")
        mintCompany = objContext.GetASPSessionValue("nMultiCompany")
        clsConfig = Nothing
    End Sub
	
	Public Function Load(ByVal sCodispl As String, ByVal nErrorNum As Integer, ByVal bPuntual As Boolean) As String
		Dim strKey As String
		Dim strBuffer As String
		
		If mblnCacheEnabled Then
			If mstrCodispl > String.Empty And mstrCodispl <> sCodispl And mblnChange Then
                eRemoteDB.FileSupport.SaveBufferToFile(mstrCachePath & "\Messages\" & sCodispl & "_" & Threading.Thread.CurrentThread.CurrentCulture.Name & ".xml", mstrMessage)
				mstrMessage = String.Empty
			End If
			
			mstrCodispl = sCodispl
			If mstrMessage = String.Empty Then
                mstrMessage = eRemoteDB.FileSupport.LoadFileToText(mstrCachePath & "\Messages\" & sCodispl & "_" & Threading.Thread.CurrentThread.CurrentCulture.Name & ".xml")
			End If
			If mstrMessage = String.Empty Then
				Load = String.Empty
			Else
				strKey = CStr(nErrorNum)
				If bPuntual Then
					strKey = strKey & "A"
				Else
					strKey = strKey & "G"
				End If
				strBuffer = GetBlock(mstrMessage, "Item", True, strKey)
				Load = GetBlock(strBuffer, "Body", True)
				nErrorType = CShort("0" & GetBlock(strBuffer, "Type", True))
			End If
		Else
			Load = String.Empty
		End If
	End Function
	
    Public Sub Add(ByVal sCodispl As String, ByVal nErrorNum As Integer, ByVal bPuntual As Boolean, ByVal sMessage As String, ByVal nErrorType As Errors.ErrorsType)
        Dim strType As String

        If mblnCacheEnabled Then
            If bPuntual Then
                strType = "A"
            Else
                strType = "G"
            End If
            mstrMessage = mstrMessage & "<Item Key=""" & CStr(nErrorNum) & strType & """>" & "<Type>" & nErrorType & "</Type>" & "<Body>" & sMessage & "</Body>" & "</Item>"
            mblnChange = True
        End If
    End Sub
	
    Protected Overrides Sub Finalize()
        If mstrMessage <> String.Empty And mblnChange Then
            eRemoteDB.FileSupport.SaveBufferToFile(mstrCachePath & "\Messages\" & mstrCodispl & "_" & mCurrentCulture.Name & ".xml", mstrMessage,,, mintCompany)
        End If
        MyBase.Finalize()
    End Sub
	
    Private Function GetBlock(ByRef sSource As String, ByVal sTag As String, Optional ByVal bNotDelete As Boolean = False, Optional ByVal sKey As String = "") As String
        Dim strLabel As String
        Dim lngIniPosition As Integer
        Dim lngEndPosition As Integer

        strLabel = "<" & sTag
        If sKey = String.Empty Then
            strLabel = strLabel & ">"
        Else
            strLabel = strLabel & " Key=""" & sKey & """>"
        End If
        strLabel = UCase(strLabel)
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






