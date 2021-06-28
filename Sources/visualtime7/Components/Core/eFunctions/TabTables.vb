Option Strict Off
Option Explicit On
Friend Class TabTables
	
	Public sKey As String
	Public sDesc_item As String
	Public sDescript As String
	Public sIndSp As String
	Public sDs_select As String
	Public sShowNum As String
	
	Private mstrTabTables As String
	Private mstrCachePath As String
	Private mblnCacheEnabled As Boolean
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
        Dim clsConfig As New eRemoteDB.VisualTimeConfig
		
        mstrCachePath = clsConfig.LoadSetting("Cache", "C:\VisualTIMENet\VTimeNet\Cache", "Paths")
		mblnCacheEnabled = (UCase(clsConfig.LoadSetting("CacheEnabled", "Yes", "Database")) = "YES")
		
		If mblnCacheEnabled Then
            mstrTabTables = eRemoteDB.FileSupport.LoadFileToText(mstrCachePath & "\Tables\TabTables.xml")
			If mstrTabTables = String.Empty Then
				Call LoadTabTables()
				
                eRemoteDB.FileSupport.SaveBufferToFile(mstrCachePath & "\Tables\TabTables.xml", mstrTabTables)
			End If
		End If
		
		'UPGRADE_NOTE: Object clsConfig may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		clsConfig = Nothing
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	Private Function LoadTabTables(Optional ByVal sCode As String = "") As Boolean
		Dim recTab_tables As eRemoteDB.Query
		Dim strCondition As String
		Dim strTabCode As String
		
		If sCode <> String.Empty Then
			strCondition = "sTab_code='" & UCase(sCode) & "'"
		Else
			strCondition = String.Empty
		End If
		
		recTab_tables = New eRemoteDB.Query
		
		With recTab_tables
			If .OpenQuery("tab_tables",  , strCondition) Then
				Do While Not .EndQuery
					strTabCode = Trim(.FieldToClass("sTab_code"))
					sKey = Trim(.FieldToClass("sKey"))
					sDesc_item = Trim(.FieldToClass("sDesc_item"))
					sDescript = Trim(.FieldToClass("sDescript"))
					sIndSp = .FieldToClass("sIndSp")
					sDs_select = Trim(.FieldToClass("sDs_select"))
					sShowNum = .FieldToClass("sShowNum")
					
					mstrTabTables = mstrTabTables & "<Item Key=""" & strTabCode & """>" & "<sKey>" & sKey & "</sKey>" & "<sDesc_item>" & sDesc_item & "</sDesc_item>" & "<sDescript>" & sDescript & "</sDescript>" & "<sIndSp>" & sIndSp & "</sIndSp>" & "<sDs_select>" & sDs_select & "</sDs_select>" & "<sShowNum>" & sShowNum & "</sShowNum>" & "</Item>"
					.NextRecord()
				Loop 
				recTab_tables.CloseQuery()
				LoadTabTables = True
			End If
		End With
		'UPGRADE_NOTE: Object recTab_tables may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		recTab_tables = Nothing
	End Function
	
	Public Function Load(ByVal sCode As String) As Boolean
		Dim strBlock As String
		
		If mstrTabTables = String.Empty Then
			Load = LoadTabTables(sCode)
		Else
			strBlock = GetBlock(mstrTabTables, "Item", True, sCode)
			If strBlock = String.Empty Then
				Load = False
			Else
				sKey = GetBlock(strBlock, "sKey", True)
				sDesc_item = GetBlock(strBlock, "sDesc_item", True)
				sDescript = GetBlock(strBlock, "sDescript", True)
				sIndSp = GetBlock(strBlock, "sIndSp", True)
				sDs_select = GetBlock(strBlock, "sDs_select", True)
				sShowNum = GetBlock(strBlock, "sShowNum", True)
				Load = True
			End If
		End If
	End Function

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






