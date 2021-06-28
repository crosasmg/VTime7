Option Strict Off
Option Explicit On
Public Class Opt_ledger
	'%-------------------------------------------------------%'
	'% $Workfile:: Opt_ledger.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:18p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'Column_name                   Type    Computed  Length Prec Scale Nullable  TrimTrailingBlanks FixedLenNullInSource
	Public nInitDay As Integer ' int        no       4     10    0     yes          (n/a)             (n/a)
	Public nInitMonth As Integer ' int        no       4     10    0     yes          (n/a)             (n/a)
	Public nEndDay As Integer ' int        no       4     10    0     yes          (n/a)             (n/a)
	Public nEndMonth As Integer ' int        no       4     10    0     yes          (n/a)             (n/a)
	Public nUsercode As Integer ' smallint   no       2      5    0     no           (n/a)             (n/a)
	
	Public nYear_Fin700 As Integer
	
	'**-Auxiliar variable
	'**-Control the execution of the Find method (because this method doesn´t have parameters)
	'- Variable auxiliar
	'- Controla la ejecucion del metodo Find (debido a que este metodo no tiene parametros)
	
	Private mblnFind As Boolean ' boolean
	
	'**% Find:  Obtains the data of the Option Installation Accountant table
	'% Find: Permite obtener los datos de la tabla de Opciones de Instalacion
	'% de Contabilidad
	Public Function Find() As Boolean
		
		'**-Defines the variable lrecreaOpt_ledger
		'- Se define la variable lrecreaOpt_ledger
		
		Dim lrecreaOpt_ledger As eRemoteDB.Execute
		
		'**+Parameters definition for the stored procedure 'insudb.reaOpt_ledger'
		'**+Data read on 08/16/2000 03:27:41 PM
		'+ Definicion de parametros para stored procedure 'insudb.reaOpt_ledger'
		'+ Informacion leida el 16/08/2000 03:27:41 PM
		
		If Not mblnFind Then
			lrecreaOpt_ledger = New eRemoteDB.Execute
			
			With lrecreaOpt_ledger
				.StoredProcedure = "reaOpt_ledger"
				
				If .Run Then
					nInitDay = .FieldToClass("nInitDay")
					nInitMonth = .FieldToClass("nInitMonth")
					nEndDay = .FieldToClass("nEndDay")
					nEndMonth = .FieldToClass("nEndMonth")
					nYear_Fin700 = .FieldToClass("nYear_Fin700")
					.RCloseRec()
					mblnFind = True
				End If
			End With
		End If
		
		Find = mblnFind
		'UPGRADE_NOTE: Object lrecreaOpt_ledger may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaOpt_ledger = Nothing
	End Function
End Class






