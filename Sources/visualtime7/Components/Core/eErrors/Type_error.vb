Option Strict Off
Option Explicit On
Public Class Type_error
	'Column_name                                                                                                                      Type                                                                                                                             Computed                            Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource                Collation
	Public nType_err As Short '                                                                                                                       smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public sDescript As String '                                                                                                                    char                                                                                                                             no                                  50                      no                                  yes                                 no                                  SQL_Latin1_General_CP1_CI_AS
	Public sShort_des As String '                                                                                                                      char                                                                                                                             no                                  12                      no                                  yes                                 no                                  SQL_Latin1_General_CP1_CI_AS
	Public sStatRegt As String '                                                                                                                      char                                                                                                                             no                                  1                       no                                  yes                                 no                                  SQL_Latin1_General_CP1_CI_AS
	Public sTransitI As String '                                                                                                                      char                                                                                                                             no                                  1                       no                                  yes                                 no                                  SQL_Latin1_General_CP1_CI_AS
	Public dCompDate As Date '                                                                                                                      datetime                                                                                                                         no                                  8                       no                                  (n/a)                               (n/a)                               NULL
	Public nUserCode As Short '                                                                                                                      smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public nTypeErr_pa As Short '                                                                                                                      smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)                               NULL
	
	'**- Definition of array record type
	'- Definición de arreglo tipo registro
	Private Structure udtTab_TypErr
		Dim nType_err As Short
		Dim sDescript As String
		Dim sShort_des As String
		Dim sStatRegt As String
		Dim sTransitI As String
		Dim dCompDate As Date
		Dim nUserCode As Short
		Dim nTypeErr_pa As Short
	End Structure
	
	'**- Array
	'- Arreglo
	
	'**-Objective:
	'-Objetivo:
	Private arrTab_TypErr() As udtTab_TypErr
	
	
	Public Function Find() As Boolean
		Dim lrecreaErrors As eRemoteDB.Execute
		Dim lintCount As Short
		
		lrecreaErrors = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaErrors'
		'Información leída el 27/06/2000 02:24:14 PM
		
		With lrecreaErrors
			.StoredProcedure = "ReaTab_TypErr"
			'.Parameters.Add "nType_err", nTypeErr, rdbParamInput, rdbSmallInt, 0, 0, 5, rdbParamNullable
			
			If .Run(True) Then
				ReDim arrTab_TypErr(200)
				Do While Not .EOF
					lintCount = lintCount + 1
					arrTab_TypErr(lintCount).nType_err = .FieldToClass("nType_err")
					arrTab_TypErr(lintCount).sDescript = .FieldToClass("sDescript")
					arrTab_TypErr(lintCount).sShort_des = .FieldToClass("sShort_des")
					arrTab_TypErr(lintCount).sStatRegt = .FieldToClass("sStatRegt")
					arrTab_TypErr(lintCount).sTransitI = .FieldToClass("sTransitI")
					arrTab_TypErr(lintCount).dCompDate = .FieldToClass("dCompDate")
					arrTab_TypErr(lintCount).nUserCode = .FieldToClass("nUserCode")
					arrTab_TypErr(lintCount).nTypeErr_pa = .FieldToClass("nTypeErr_pa")
					.RNext()
				Loop 
				Find = True
                ReDim Preserve arrTab_TypErr(lintCount)
                .RCloseRec()
			Else
				Find = False
			End If
		End With
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		
		lrecreaErrors = Nothing
	End Function
	
	
	Public ReadOnly Property Count() As Short
		Get
			Count = UBound(arrTab_TypErr)
			
		End Get
	End Property
	
	Public Function Item(ByVal nPosition As Short) As Boolean
		
		If nPosition <= Me.Count Then
			With arrTab_TypErr(nPosition)
				Me.nType_err = .nType_err
				Me.sDescript = .sDescript
				Me.sShort_des = .sShort_des
				Me.sStatRegt = .sStatRegt
				Me.sTransitI = .sTransitI
				Me.dCompDate = .dCompDate
				Me.nUserCode = .nUserCode
				Me.nTypeErr_pa = .nTypeErr_pa
			End With
			Item = True
		Else
			Item = False
		End If
		
	End Function
End Class











