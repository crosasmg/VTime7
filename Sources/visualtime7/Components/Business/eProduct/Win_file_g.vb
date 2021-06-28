Option Strict Off
Option Explicit On
Public Class Win_file_g
	'%-------------------------------------------------------%'
	'% $Workfile:: Win_file_g.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'**- Definition of publics variables of the class accourding to the structure of the table
	'**-Win_file_g 08/11/2001
	
	'- Definición de variables públicas de la clase según la estructura de la tabla
	'- Win_file_g 08/11/2001
	
	'    Column_name                         Type          Computed     Length      Prec  Scale  Nullable      TrimTrailingBlanks       FixedLenNullInSource
	'------------------------------------  ------------  ------------  ---------  ------- ------ ---------- ----------------------- ---------------------------
	Public sCodispl As String ' char            no           8                       no                no                          no
	Public nBranch_gen As Integer ' smallint        no           2           5     0     no               (n/a)                       (n/a)
	Public dCompdate As Date ' datetime        no           8                       yes              (n/a)                       (n/a)
	Public sTabname As String ' char            no           20                      yes               no                          yes
	Public nUsercode As Integer ' smallint        no           2           5     0     yes              (n/a)                       (n/a)
	
	Public sDescript As String
	
	Private Structure udtWinFile_g
		Dim sCodispl As String
		Dim sTabname As String
		Dim sDescript As String
		Dim nBranch_gen As Integer
	End Structure
	
	Private arrWinFile_g() As udtWinFile_g
	
	'**%CountItemMDP002: property that indicate the record number that is place in a moment in the class arrengement
	'%CountItemMDP002: propiedad que indica el número de registros que se encuentra en determinado momento en el arreglo de la clase
	Public ReadOnly Property CountItemMDP002() As Integer
		Get
			CountItemMDP002 = UBound(arrWinFile_g)
		End Get
	End Property
	
	'**%ItemMDP002: function that considers the index value charge the array information in the class variables
	'%ItemMDP002: Función que tomando en cuenta el valor del index carga en las variables de la clase la información del arreglo
	Public Function ItemMDP002(ByVal lintIndex As Integer) As Boolean
		If lintIndex <= UBound(arrWinFile_g) Then
			With arrWinFile_g(lintIndex)
				sCodispl = .sCodispl
				sTabname = .sTabname
				sDescript = .sDescript
				nBranch_gen = .nBranch_gen
			End With
			ItemMDP002 = True
		Else
			ItemMDP002 = False
		End If
		
	End Function
	
	'%Find: Esta función se encarga de leer la información en tratamiento de la
	'%tabla principal para la transacción.
	Public Function Find() As Boolean
		On Error GoTo Find_Err
		
		Dim lrecreaWinFile_g As eRemoteDB.Execute
		lrecreaWinFile_g = New eRemoteDB.Execute
		
		Dim lintCount As Integer
		
		'+ Definición de parámetros para stored procedure 'insudb.reaWinFile_g'
		'+ Información leída el 08/11/2001 04:39:47 p.m.
		
		With lrecreaWinFile_g
			.StoredProcedure = "reaWinFile_g"
			If .Run Then
				ReDim arrWinFile_g(1000)
				lintCount = 0
				Do While Not .EOF
					arrWinFile_g(lintCount).sCodispl = .FieldToClass("sCodispl")
					arrWinFile_g(lintCount).sDescript = .FieldToClass("sDescript")
					arrWinFile_g(lintCount).sTabname = .FieldToClass("sTabname")
					arrWinFile_g(lintCount).nBranch_gen = .FieldToClass("nBranch_gen")
					lintCount = lintCount + 1
					.RNext()
				Loop 
				.RCloseRec()
				ReDim Preserve arrWinFile_g(lintCount)
				Find = True
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaWinFile_g may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaWinFile_g = Nothing
	End Function
	
	'%Add: Esta función se encarga de agregar la información en tratamiento de la
	'%tabla principal para la transacción.
	Public Function Add() As Boolean
		On Error GoTo Add_err
		
		Dim lrecinsWin_file_g As eRemoteDB.Execute
		
		lrecinsWin_file_g = New eRemoteDB.Execute
		
		Add = True
		
		'+ Definición de parámetros para stored procedure 'insudb.insWin_file_g'
		'+ Información leída el 12/11/2001 03:07:23 p.m.
		
		With lrecinsWin_file_g
			.StoredProcedure = "insWin_file_g"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_gen", nBranch_gen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTabname", sTabname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lrecinsWin_file_g may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsWin_file_g = Nothing
	End Function
	
	'%insExistTable: Esta función valida la existencia de la tabla de Datos particulares del ramo
	'%en la base de datos.
	Private Function insExistTable(ByRef strTabname As String) As Boolean
		On Error GoTo insExistTable_Err
		
		Dim lrecreaSysobjects_v As eRemoteDB.Execute
		
		lrecreaSysobjects_v = New eRemoteDB.Execute
		
		insExistTable = True
		
		'+ Definición de parámetros para stored procedure 'insudb.reaSysobjects_v'
		'+ Información leída el 12/11/2001 03:32:39 p.m.
		
		With lrecreaSysobjects_v
			.StoredProcedure = "reaSysobjects_v"
			.Parameters.Add("strTabname", strTabname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				.RCloseRec()
				insExistTable = True
			Else
				insExistTable = False
			End If
		End With
		
insExistTable_Err: 
		If Err.Number Then
			insExistTable = False
		End If
		'UPGRADE_NOTE: Object lrecreaSysobjects_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSysobjects_v = Nothing
	End Function
	
	'%insPostMDP002: Esta función que devuelve VERDADERO cuando se realizan las actualizaciones en la tabla principal
	Public Function insPostMDP002(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch_gen As Integer, ByVal sTabname As String, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostMDP002_Err
		
		insPostMDP002 = True
		
		With Me
			.sCodispl = sCodispl
			.nBranch_gen = nBranch_gen
			.sTabname = sTabname
			.nUsercode = nUsercode
			
			If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
				insPostMDP002 = .Add
			End If
		End With
		
insPostMDP002_Err: 
		If Err.Number Then
			insPostMDP002 = False
		End If
	End Function
	
	'%insValMDP002: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
	Public Function insValMDP002(ByVal sCodispl As String, ByVal nBranch_gen As Integer, ByVal sTabname As String) As String
		On Error GoTo insValMDP002_Err
		
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		'+ Validacion de la tabla de Datos Particulares
		If sTabname = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 10158)
		Else
			If Not insExistTable(sTabname) Then
				Call lclsErrors.ErrorMessage(sCodispl, 3341)
			End If
		End If
		
		'+ Validacion del Ramo generico
		If nBranch_gen = 0 Or nBranch_gen = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 11227)
		End If
		
		insValMDP002 = lclsErrors.Confirm
		
insValMDP002_Err: 
		If Err.Number Then
			insValMDP002 = insValMDP002 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
End Class






