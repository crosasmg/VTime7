Option Strict Off
Option Explicit On
Public Class Ctrol_date
	'%-------------------------------------------------------%'
	'% $Workfile:: Ctrol_date.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:18p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'**- The properties of the class are defined.
	'-   Se definen las propiedades de la clase.
	
	'**- Column_name.                                                                                                                 Type                                                                                                                             Computed                            Length      Prec  Scale  Nullable                            TrimTrailingBlanks                  FixedLenNullInSource                Collation
	'-   Nombre de la columna                                                                                                         Tipo                                                                                                                             Computed                            Longitud    Prec  Escala Admite nulos                        TrimTrailingBlanks                  FixedLenNullInSource                Collation
	Public nType_proce As Integer '                                                                                            smallint                                                                                                                         no                                  2           5     0      no                                  (n/a)                               (n/a)                               NULL
	Public dEffecdate As Date '                                                                                            datetime                                                                                                                         no                                  8                        yes                                 (n/a)                               (n/a)                               NULL
	Public nUsercode As Integer '                                                                                            smallint                                                                                                                         no                                  2           5     0      yes                                 (n/a)                               (n/a)                               NULL
	
	'**% Find: search the table for the last date of processing.
	'% Find: Permite buscar registros en la tabla de Fecha de últimos procesos.
	Public Function Find(ByVal lintType_proce As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecReaCtrol_date As eRemoteDB.Execute
		Static lblnRead As Boolean
		
		lrecReaCtrol_date = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If nType_proce <> lintType_proce Or lblnFind Then
			
			nType_proce = lintType_proce
			
			'**+Parameters defintion for the stored procedure 'insudb.reaCtrol_Date'
			'**+Data read on 05/23/2001 04:27:54 PM
			'+ Definición de parámetros para stored procedure 'insudb.reaCtrol_Date'
			'+ Información leída el 23/05/2001 04:27:54 PM
			
			With lrecReaCtrol_date
				.StoredProcedure = "reaCtrol_Date"
				
				.Parameters.Add("nType_proce", lintType_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run(True) Then
					dEffecdate = .FieldToClass("dEffecdate")
					
					lblnRead = True
					.RCloseRec()
				Else
					lblnRead = False
				End If
			End With
		End If
		
		Find = lblnRead
		
		'UPGRADE_NOTE: Object lrecReaCtrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCtrol_date = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
	End Function
End Class






