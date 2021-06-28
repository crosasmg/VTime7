Option Strict Off
Option Explicit On
Public Class Opt_system
	'%-------------------------------------------------------%'
	'% $Workfile:: Opt_system.cls                           $%'
	'% $Author:: Mvazquez                                   $%'
	'% $Date:: 14/03/06 19:35                               $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'**+Properties accourding to the system on 11/03/2000
	'+ Propiedades según la tabla en el sistema el 03/11/2000
	
	'+  Column_name               Type                   Computed Length      Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ -------------------------- ---------------------- -------- ----------- ----- ----- -------- ------------------ --------------------
	Public dInit_date As Date 'datetime no       8                       yes      (n/a)              (n/a)
	Public sPrint_tx_c As String 'char     no       1                       yes      no                 yes
	Public sQ_value As String 'char     no       1                       yes      no                 yes
	Public nModules As Integer 'smallint no       2           5     0     yes      (n/a)              (n/a)
	Public dEffecdate As Date 'datetime no       8                       no       (n/a)              (n/a)
	Public nLanguage As Integer 'smallint no       2           5     0     yes      (n/a)              (n/a)
	Public sFormatPer As String 'char     no       13                      yes      no                 yes
	Public dInitMod As Date 'datetime no       8                       yes      (n/a)              (n/a)
	Public sFormatComp As String 'char     no       13                      yes      no                 yes
	Public nCountry As Integer 'smallint no       2           5     0     yes      (n/a)              (n/a)
	Public sPolicyNum As String 'char     no       1                       yes      no                 yes
	Public sClaimNum As String 'char     no       1                       yes      no                 yes
	Public sReceiptNum As String 'char     no       1                       yes      no                 yes
	Public nCompany As Integer 'smallint no       2           5     0     yes      (n/a)              (n/a)
	Public sSecure As String 'char     no       1                       yes      no                 yes
	Public nInsur_Area As Integer
	Public nNum_Fem As Integer
	Public dLastDate As Date
	
	'**+Auxiliary properties
	'+ Propiedades auxiliares
	'**+Company type (Table219)
	Public sTypeCompany As String '+ Tipo de Compañia (Table219)
	
	'**% Find: makes the reading of the table
	'% Find: se realiza la lectura de la tabla
	Public Function find() As Boolean
		Dim lrecreaOpt_System As eRemoteDB.Execute
		
		lrecreaOpt_System = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'**+ Parameters definition for the stored procedure 'insudb.reaOpt_System'
		'**+Data read on 11/03/2000 04:32:07 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaOpt_System'
		'+ Información leída el 03/11/2000 04:32:07 p.m.
		
		With lrecreaOpt_System
			.StoredProcedure = "reaOpt_System"
			If .Run Then
				dInit_date = .FieldToClass("dInit_date")
				sPrint_tx_c = .FieldToClass("sPrint_tx_c")
				sQ_value = .FieldToClass("sQ_value")
				nModules = .FieldToClass("nModules")
				dEffecdate = .FieldToClass("dEffecdate")
				nLanguage = .FieldToClass("nLanguage")
				sFormatPer = .FieldToClass("sFormatPer")
				dInitMod = .FieldToClass("dInitMod")
				sFormatComp = .FieldToClass("sFormatComp")
				nCountry = .FieldToClass("nCountry")
				sPolicyNum = .FieldToClass("sPolicyNum")
				sClaimNum = .FieldToClass("sClaimNum")
				sReceiptNum = .FieldToClass("sReceiptNum")
				nCompany = .FieldToClass("nCompany")
				sSecure = .FieldToClass("sSecure")
				sTypeCompany = .FieldToClass("sTypeCompany")
				nInsur_Area = .FieldToClass("nInsur_Area")
				nNum_Fem = .FieldToClass("nNum_Fem")
				dLastDate = .FieldToClass("dLastDate")
				find = True
				.RCloseRec()
			Else
				find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaOpt_System may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaOpt_System = Nothing
		
Find_Err: 
		If Err.Number Then
			find = False
		End If
		On Error GoTo 0
	End Function
End Class






