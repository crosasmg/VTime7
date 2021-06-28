Option Strict Off
Option Explicit On
Public Class TmpReportMasive
	'%-------------------------------------------------------%'
    '% $Workfile:: TmpReportMasive.cls                               $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
    '+ Propiedades según la tabla en el sistema al 30/11/2000.
    '   Column_name                        Type         Computed   Length  Prec  Scale  Nullable   TrimTrailingBlanks    FixedLenNullInSource
    Public sKey As String 'char             no         1                   no               no                     no
    Public nId As Long 'char             no         1                   no               no                     no
    Public sCertype As String 'char            no         1                   no               no                     no
    Public nBranch As Integer 'smallint        no         2        5    0     no               (n/a)                  (n/a)
    Public nProduct As Integer 'smallint        no         2        5    0     no               (n/a)                  (n/a)
    Public nPolicy As Double 'int             no         4       10    0     no               (n/a)                  (n/a)
    Public nCertif As Double 'int             no         4       10    0     yes              (n/a)                  (n/a)
    Public dDate_origi As Date
    Public dStartdate As Date
    Public nType_amend As Integer
    Public nStatus As Integer
    Public dDate_printer As Date
    Public dCompdate As Date
    Public sExecutiontype As String
    Public sClient As String
    Public dAprobdate As Date
    Public sTypereport As String
    Public nFolionum As Integer

	'-Variable auxiliar para verificar la transacción
	Public sCodispl As String
	


	
    '%Find: Obtiene la información de la cobertura
	Public Function Find(ByVal sKey As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal sClient As String, Optional ByVal bAll As Boolean = False) As Boolean
		Dim lrecreatcover As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If Me.sKey <> sKey Or Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or bAll Then
			
			lrecreatcover = New eRemoteDB.Execute
			
			With lrecreatcover
                .StoredProcedure = "REA_TMP_REPORT_MASIVE"
				.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then

                    Find = True
                    sKey = .FieldToClass("sKey")
                    nId = .FieldToClass("nId")
                    sCertype = .FieldToClass("sCertype")
                    nBranch = .FieldToClass("nBranch")
                    nProduct = .FieldToClass("nProduct")
                    nPolicy = .FieldToClass("nPolicy")
                    nCertif = .FieldToClass("nCertif")
                    dDate_origi = .FieldToClass("dDate_origi")
                    dStartdate = .FieldToClass("dStartdate")
                    nType_amend = .FieldToClass("nType_amend")
                    nStatus = .FieldToClass("nStatus")
                    dDate_printer = .FieldToClass("dDate_printer")
                    dCompdate = .FieldToClass("dCompdate")
                    sExecutiontype = .FieldToClass("sExecutiontype")
                    sClient = .FieldToClass("sClient")
                    dAprobdate = .FieldToClass("dAprobdate")
                    sTypereport = .FieldToClass("sTypereport")
                    nFolionum = .FieldToClass("nFolionum")
                    
                End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreatcover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreatcover = Nothing
	End Function
	

End Class






