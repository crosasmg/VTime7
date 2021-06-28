Option Strict Off
Option Explicit On
Public Class Field
	'%-------------------------------------------------------%'
	'% $Workfile:: Field.cls                                $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.34                               $%'
	'% $Revision:: 28                                       $%'
	'%-------------------------------------------------------%'
	
	'Column_name                      Type        Length      Prec  Scale Nullable
	'-------------------------------- ----------- ----------- ----- ----- ---------
	Public sKey As String 'char       20                       yes
	Public nRow As Integer 'int        4           10    0     no
	Public nColumn As Integer 'int        4           10    0     no
	Public sField As String 'char       20                      no
	Public sValue As String 'char       300                     no
	Public sTable As String 'char       20                      yes
	Public sProcess As String 'char       30                      yes
	
	Private Const clngActionadd As String = "301" '+  Registrar
	Private Const clngActionUpdate As String = "302" '+  Actualizar
	Private Const clngActioncut As String = "303" '+  Eliminar
	
	Private mobjGrid As eFunctions.Grid
	Private Structure udtArray
		Dim sField As String
	End Structure
	Public mlngIndex As Integer
	Private marray() As udtArray
	
	'% MakeGI1403 : Construye el Grid que muestra la Tabla Temporal T_INTERFACE
	Public Function MakeGI1403(ByVal sKey As String, ByVal nSheet As Integer, ByVal nRow As Integer, Optional ByVal nType As Integer = 0) As String
		
		Dim llngIndex As Integer
		Dim nFieldType As Integer
		Dim lcolField As Fields
		Dim lclsField As Field
        Dim lobjValues As eFunctions.Values
        Dim strResult As String = ""

        Try

            mobjGrid = New eFunctions.Grid
            lcolField = New Fields
            lclsField = New Field
            lobjValues = New eFunctions.Values

            nFieldType = nType
            If nFieldType <= 0 Then
                nFieldType = 2
            End If
            Call insDefineHeader(nSheet, nFieldType)
            nRow = IIf(nRow = numNull, 1, nRow)
            If lcolField.Find(sKey, nRow, nFieldType) Then
                llngIndex = 1
                For Each lclsField In lcolField
                    With mobjGrid
                        If llngIndex = mlngIndex + 1 Then
                            strResult = strResult & .DoRow
                            llngIndex = 1
                        End If
                        .Columns(marray(llngIndex).sField).DefValue = lclsField.sValue
                        llngIndex = llngIndex + 1
                    End With
                Next lclsField
                strResult = strResult & mobjGrid.DoRow
            End If
            strResult = strResult & mobjGrid.closeTable
            Return strResult
        Catch ex As Exception
            MakeGI1403 = Err.Description
        Finally
            'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            mobjGrid = Nothing
            'UPGRADE_NOTE: Object lcolField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lcolField = Nothing
            'UPGRADE_NOTE: Object lclsField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsField = Nothing
            'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lobjValues = Nothing
        End Try

    End Function
	
	'% insDefineHeader: se definen las propiedades del grid
	Private Sub insDefineHeader(ByVal nSheet As Integer, Optional ByVal nType As Integer = 0)
		'-Coleción que trae los Encabezado de la grilla
		Dim lcolColSheet As FieldSheets
		Dim lclsColsheet As FieldSheet
		Dim llngIndex As Integer
		Dim nFieldType As Integer
		'-Marcador de columna criterio de búsqueda
		Dim lstrIndCriter As String
		
		nFieldType = nType
		If nFieldType <= 0 Then
			nFieldType = 2
		End If
		
		lcolColSheet = New FieldSheets
		
		Call lcolColSheet.Find(nSheet, nFieldType)
		
		mlngIndex = lcolColSheet.Count
		
		ReDim Preserve marray(mlngIndex)
		
		mobjGrid = New eFunctions.Grid
		'+ Se definen las columnas del grid
		
		With mobjGrid.Columns
			llngIndex = 1
			For	Each lclsColsheet In lcolColSheet
				lstrIndCriter = ""
				
				Call .AddTextColumn(0, lstrIndCriter & " " & lclsColsheet.sFieldDesc, lclsColsheet.sColumnName & llngIndex, lclsColsheet.nFieldLarge, "",  ,  ,  ,  ,  , True)
				marray(llngIndex).sField = lclsColsheet.sColumnName & llngIndex
				llngIndex = llngIndex + 1
			Next lclsColsheet
		End With
		
		
		'+ Se definen las propiedades generales del grid
		With mobjGrid
			.Codispl = "GI1403"
			.DeleteButton = False
			.AddButton = False
			.Top = 50
			.Height = 430
			.Width = 400
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		End With
		
		'UPGRADE_NOTE: Object lcolColSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolColSheet = Nothing
		
	End Sub
End Class






