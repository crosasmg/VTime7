Option Strict Off
Option Explicit On
Public Class Detail_pre
	'%-------------------------------------------------------%'
	'% $Workfile:: Detail_pre.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:06p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema al 13/11/2000
	'+ Los campos llave de la tabla corresponden a: sCertype, nBranch, nProduct, nReceipt, nDigit, nPaynumbe, sType_detai y nDet_code.
	
	Private Structure udtDetail_pre
		Dim dStartdate As Date
		Dim dExpirDate As Date
		Dim nCurrency As Integer
		Dim nCommi_rate As Double
		Dim sType_detai As String
		Dim nCommision As Double
		Dim nPremium As Double
		Dim nPremAnual As Double
		Dim sDescript As String
		Dim nBill_item As Integer
		Dim nAmountAf As Double
		Dim nAmountEx As Double
	End Structure
	
	Public nAmountAf As Double
	Public nAmountEx As Double
	
	'   Column_name                     Type      Computed   Length  Prec  Scale Nullable   TrimTrailingBlanks    FixedLenNullInSource
	Public sCertype As String 'char        no        1                   no              no                      no
	Public nReceipt As Integer 'int         no        4      10     0     no              (n/a)                   (n/a)
	Public nBranch As Integer 'smallint    no        2       5     0     no              (n/a)                   (n/a)
	Public nDigit As Integer 'smallint    no        2       5     0     no              (n/a)                   (n/a)
	Public nProduct As Integer 'smallint    no        2       5     0     no              (n/a)                   (n/a)
	Public nPaynumbe As Integer 'smallint    no        2       5     0     no              (n/a)                   (n/a)
	Public sType_detai As String 'char        no        1                   no              no                      no
	Public nDet_code As Integer 'smallint    no        2       5     0     no              (n/a)                   (n/a)
	Public sAddsuini As String 'char        no        1                   yes             no                      yes
	Public nCapital As Double 'decimal     no        9      12     0     yes             (n/a)                   (n/a)
	Public nCommi_rate As Double 'decimal     no        5       4     2     yes             (n/a)                   (n/a)
	Public nCommision As Double 'decimal     no        9      10     2     yes             (n/a)                   (n/a)
	Public nPremium As Double 'decimal     no        9      10     2     yes             (n/a)                   (n/a)
	Public nTax As Double 'decimal     no        9      10     2     yes             (n/a)                   (n/a)
	Public nBill_item As Integer 'smallint    no        2       5     0     yes             (n/a)                   (n/a)
	Public nBranch_est As Integer 'smallint    no        2       5     0     no              (n/a)                   (n/a)
	Public nBranch_led As Integer 'smallint    no        2       5     0     no              (n/a)                   (n/a)
	Public nBranch_rei As Integer 'smallint    no        2       5     0     yes             (n/a)                   (n/a)
	Public nUsercode As Integer 'smallint    no        2       5     0     yes             (n/a)                   (n/a)
	Public nPremAnual As Double 'decimal     no        9      10     2     yes             (n/a)                   (n/a)
	Public nComAnual As Double 'decimal     no        9      10     2     yes             (n/a)                   (n/a)
	
	'- Arreglo para la carga de recibos
	
	Private marrReceipts() As udtDetail_pre
	
	'- Indica si el arreglo de recibos se cargo o no
	
	Private mblnCharge As Boolean
	
	'- Variables auxialiares
	
	Public dStartdate As Date
	Public dExpirDate As Date
	Public nCurrency As Integer
	Public sDescript As String
	
	'% CountReceipts: Devuelve el número de recibos que se encuentran en el arreglo
	Public ReadOnly Property CountReceipts() As Integer
		Get
			
			If mblnCharge Then
				CountReceipts = UBound(marrReceipts)
			Else
				CountReceipts = -1
			End If
		End Get
	End Property

    '% LoadReceipts: Devuelve los conceptos de facturación de los recibos emitidos
    Public Function LoadReceipts(ByVal sCertype As String, ByVal nReceipt As Double, ByVal nDigit As Integer, ByVal nPaynumbe As Integer, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean

        '- Se define la variable lrecreaReceiptGen

        Dim lrecreaReceiptGen As eRemoteDB.Execute
        Dim llngIndex As Integer

        lrecreaReceiptGen = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaReceiptGen'
        '+ Información leída el 17/11/2000 16:12:15

        With lrecreaReceiptGen
            .StoredProcedure = "reaReceiptGen"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPayNumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                llngIndex = -1
                LoadReceipts = True
                mblnCharge = True

                ReDim marrReceipts(100)

                Do While Not .EOF
                    llngIndex = llngIndex + 1

                    marrReceipts(llngIndex).dExpirDate = .FieldToClass("dExpirDate")
                    marrReceipts(llngIndex).dStartdate = .FieldToClass("dStartDate")
                    marrReceipts(llngIndex).nBill_item = .FieldToClass("nBill_item")
                    marrReceipts(llngIndex).nCommision = .FieldToClass("nCommision")
                    marrReceipts(llngIndex).nCurrency = .FieldToClass("nCurrency")
                    marrReceipts(llngIndex).nPremAnual = .FieldToClass("nPremAnual")
                    marrReceipts(llngIndex).nPremium = .FieldToClass("nPremium")
                    marrReceipts(llngIndex).sDescript = .FieldToClass("sDescript")
                    marrReceipts(llngIndex).nAmountAf = .FieldToClass("nAmountAf")
                    marrReceipts(llngIndex).nAmountEx = .FieldToClass("nAmountEx")

                    .RNext()
                Loop

                .RCloseRec()

                ReDim Preserve marrReceipts(llngIndex)
            Else
                LoadReceipts = False
                mblnCharge = False
            End If
        End With

        'UPGRADE_NOTE: Object lrecreaReceiptGen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaReceiptGen = Nothing
    End Function

    '% ReceiptItem: Carga en las variables de la clase la información de un recibo
    Public Function ReceiptItem(ByVal llngIndex As Integer) As Boolean
		
		If mblnCharge Then
			If llngIndex <= UBound(marrReceipts) Then
				With marrReceipts(llngIndex)
					dExpirDate = .dExpirDate
					dStartdate = .dStartdate
					nBill_item = .nBill_item
					nCommi_rate = .nCommi_rate
					nCommision = .nCommision
					nCurrency = .nCurrency
					nPremAnual = .nPremAnual
					nPremium = .nPremium
					sDescript = .sDescript
					sType_detai = .sType_detai
					nAmountAf = .nAmountAf
					nAmountEx = .nAmountEx
				End With
				
				ReceiptItem = True
			Else
				ReceiptItem = False
			End If
		End If
	End Function
	
	'% Find: Rescata registro de Detail_pre
	Public Function Findafex(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nReceipt As Double, ByVal nDigit As Integer, ByVal nPaynumbe As Integer) As Boolean
		Dim lrecreadetail_pre As eRemoteDB.Execute
		
		On Error GoTo Findafex_Err
		
		lrecreadetail_pre = New eRemoteDB.Execute
		Findafex = True
		
		'+ Definición de parámetros para stored procedure 'reaDetail_pre_key'
		'+ Información leída el 22/07/2002
		
		With lrecreadetail_pre
			.StoredProcedure = "reaDetail_pre_AfEx"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nAmountAf = .FieldToClass("nPremiumA")
				Me.nAmountEx = .FieldToClass("nPremiumE")
				.RCloseRec()
			End If
		End With
		
Findafex_Err: 
		If Err.Number Then
			Findafex = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreadetail_pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreadetail_pre = Nothing
	End Function
End Class






