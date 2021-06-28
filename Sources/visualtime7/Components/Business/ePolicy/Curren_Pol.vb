Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Curren_pol_NET.Curren_pol")> Public Class Curren_pol
	'%-------------------------------------------------------%'
	'% $Workfile:: Curren_Pol.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 10.47                               $%'
	'% $Revision:: 35                                       $%'
	'%-------------------------------------------------------%'
	
	'-
	'- Estructura de tabla curren_pol al 08-16-2002 09:03:27
	'-     Property                   Type         DBType   Size Scale  Prec  Null
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public nCurrency As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dCompdate As Date ' DATE       7    0     0    S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	
	Public nCount As Integer
	
	'-Propiedades auxiliares
	Private Structure udtCurren_pol
		Dim Certype As String
		Dim nBranch As Integer
		Dim nProduct As Integer
		Dim nPolicy As Double
		Dim nCertif As Double
		Dim nCurrency As Integer
		Dim dEffecdate As Date
		Dim dNulldate As Date
		Dim sDescrip As String
		Dim nExchange As Double
		Dim sDefaulti As Integer
		Dim nExist As Integer
	End Structure
	
	Private arrCurren_pol() As udtCurren_pol
	
	'-Variable que indica si es la moneda por defecto (valores posibles: "1" si es por defecto, "0" si no lo es)
	Public sDefaulti As Integer
	
	'-Variable que contiene el factor de cambio  de la moneda
	Public nExchange As Double
	
	'-Variable que contiene la descripcion de la moneda
	Public sDescript As String
	Public Existe As Integer
	
	'-Variable que devuelve la cantidad de monedas cargadas en el arreglo
	Public nCountCurPol As Integer
	
	'- Variable que indica si existen registos en curren_pol
	Public nExist As Integer
	
	'-Variable que permite referenciar a la clase de cur_allow desde curren_pol
	Public lclsCur_allow As eProduct.Cur_Allow
	
	'-Variable que indica que image debe asociarse a la ventana en la secuencia (valores posibles: "ok" si la ventana contiene informacion, "required" si la ventana es requerida)
	Public mstrImage As eFunctions.Sequence.etypeImageSequence
	
	'-Variable que indica si se cargo automaticamente las monedas
	Public mblnAutoFill As Boolean
	
	'-Variable que indica si se esta trabajando con la moneda local
	Private mblnIsLocal As Boolean
	
	'-Variable que indica si el arreglo contiene información
	Private mblnChargeArr As Boolean
	
	'-Variable que guarda la fecha de efecto de búsqueda
	Private mdtmEffecdate As Object
	
	'% FindOneOrLocal: Busca y carga la moneda de la poliza/certificado
	'%                 Si tiene mas de una moneda se carga la local
	Public Function FindOneOrLocal(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		On Error GoTo FindOneOrLocal_Err
		
		If Not Find(nPolicy, nBranch, nProduct, sCertype, nCertif, dEffecdate) Then
			'+ Si no hay moneda se carga la local
			IsLocal = True
		End If
		
		'+ Se carga la moneda en las propiedades de la clase
		FindOneOrLocal = Val_Curren_pol(0)
		
FindOneOrLocal_Err: 
		If Err.Number Then
			FindOneOrLocal = False
		End If
		On Error GoTo 0
	End Function
	
	'*IsLocal: propiedad que indica si se esta trabajando con la moneda local
	
	'*IsLocal: una vez que se indica si se esta trbajando con la moneda local esta se carga en el arreglo
	'*de la clase ...
	Public Property IsLocal() As Boolean
		Get
			IsLocal = mblnIsLocal
		End Get
		Set(ByVal Value As Boolean)
			Dim lclsQuery As eRemoteDB.Query
			lclsQuery = New eRemoteDB.Query
			
			mblnIsLocal = Value
			
			If Value Then
				ReDim arrCurren_pol(0)
				arrCurren_pol(0).nCurrency = 1
				arrCurren_pol(0).nExchange = 1
				With lclsQuery
					If .OpenQuery("Table11", "sDescript", "nCodigint = 1") Then
						arrCurren_pol(0).sDescrip = Trim(.FieldToClass("sDescript"))
						.CloseQuery()
					End If
				End With
				mblnChargeArr = True
			End If
		End Set
	End Property
	
	'*CountCurrenPol: propiedad que indica el número de monedas qe se encuentra en determinado
	'*momento en el arreglo de la clase
	Public ReadOnly Property CountCurrenPol() As Integer
		Get
			If mblnChargeArr Then
				CountCurrenPol = UBound(arrCurren_pol)
			Else
				CountCurrenPol = -1
			End If
		End Get
	End Property
	
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "Curren_pol". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lintCount As Integer
		Dim lreccreCurren_pol As eRemoteDB.Execute
		
		On Error GoTo creCurren_pol_Err
		
		lreccreCurren_pol = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.creCurren_pol'
		'+Información leída el 01/11/1999 10:47:52 AM
		
		Add = False
		With lreccreCurren_pol
			.StoredProcedure = "creCurren_pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				mblnChargeArr = True
				IsLocal = False
				Add = True
				lintCount = 0
				ReDim Preserve arrCurren_pol(10)
				While arrCurren_pol(lintCount).nBranch <> 0 And lintCount < UBound(arrCurren_pol)
					lintCount = lintCount + 1
				End While
				With arrCurren_pol(lintCount)
					.Certype = sCertype
					.nBranch = nBranch
					.nProduct = nProduct
					.nPolicy = nPolicy
					.nCertif = nCertif
					.nCurrency = nCurrency
					.dEffecdate = dEffecdate
					.dNulldate = dNulldate
					.sDescrip = sDescript
					.nExchange = nExchange
				End With
				ReDim Preserve arrCurren_pol(lintCount)
			End If
		End With
		
creCurren_pol_Err: 
		If Err.Number Then
			Add = False
		End If
		
		'UPGRADE_NOTE: Object lreccreCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreCurren_pol = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Update: Este método se encarga de actualizar registros en la tabla "Curren_pol". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		Dim lintCount As Integer
		Dim lrecupdCurren_pol As eRemoteDB.Execute
		
		lrecupdCurren_pol = New eRemoteDB.Execute
		
		On Error GoTo updCurren_pol_Err
		
		'+Definición de parámetros para stored procedure 'insudb.udtCurren_pol'
		'+Información leída el 01/11/1999 10:51:15 AM
		
		With lrecupdCurren_pol
			.StoredProcedure = "updCurren_pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lintCount = 0
				If Find(nPolicy, nBranch, nProduct, sCertype, nCertif, dEffecdate, True) Then
					Do While arrCurren_pol(lintCount).nCurrency <> nCurrency
						lintCount = lintCount + 1
					Loop 
					With arrCurren_pol(lintCount)
						.Certype = sCertype
						.nBranch = nBranch
						.nProduct = nProduct
						.nPolicy = nPolicy
						.nCertif = nCertif
						.nCurrency = nCurrency
						.dEffecdate = dEffecdate
						.dNulldate = dNulldate
					End With
				End If
			End If
		End With
		
updCurren_pol_Err: 
		If Err.Number Then
			Update = False
		End If
		
		'UPGRADE_NOTE: Object lrecupdCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdCurren_pol = Nothing
		On Error GoTo 0
	End Function
	
	'%Delete: Este método se encarga de eliminar registros en la tabla "Curren_pol". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete() As Boolean
		Dim lintCount As Integer
		Dim lrecdelCurren_pol As eRemoteDB.Execute
		
		On Error GoTo delCurren_pol_Err
		
		lrecdelCurren_pol = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.delCurren_pol'
		'+Información leída el 01/11/1999 10:52:45 AM
		
		With lrecdelCurren_pol
			.StoredProcedure = "delCurren_pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lintCount = 0
				While arrCurren_pol(lintCount).nCurrency <> nCurrency
					lintCount = lintCount + 1
				End While
				Do While lintCount < UBound(arrCurren_pol)
					With arrCurren_pol(lintCount)
						.Certype = arrCurren_pol(lintCount + 1).Certype
						.nBranch = arrCurren_pol(lintCount + 1).nBranch
						.nProduct = arrCurren_pol(lintCount + 1).nProduct
						.nPolicy = arrCurren_pol(lintCount + 1).nPolicy
						.nCertif = arrCurren_pol(lintCount + 1).nCertif
						.nCurrency = arrCurren_pol(lintCount + 1).nCurrency
						.dEffecdate = arrCurren_pol(lintCount + 1).dEffecdate
						.dNulldate = arrCurren_pol(lintCount + 1).dNulldate
					End With
					lintCount = lintCount + 1
				Loop 
				If UBound(arrCurren_pol) = 0 Then
					With arrCurren_pol(lintCount)
						.Certype = CStr(0)
						.nBranch = 0
						.nProduct = 0
						.nPolicy = 0
						.nCertif = 0
						.nCurrency = 0
						.dEffecdate = System.Date.FromOADate(0)
						.dNulldate = System.Date.FromOADate(0)
					End With
				Else
					ReDim Preserve arrCurren_pol(UBound(arrCurren_pol) - 1)
				End If
			End If
		End With
		
delCurren_pol_Err: 
		If Err.Number Then
			Delete = False
		End If
		
		'UPGRADE_NOTE: Object lrecdelCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelCurren_pol = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Curren_pol"
    Public Function Find(ByVal nPolicy As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sCertype As String, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
        Dim lintCount As Integer
        Dim lrecreaCurren_pol_tmp As eRemoteDB.Execute
        Dim lblnFound As Boolean

        On Error GoTo Find_Err
        If Me.nPolicy <> nPolicy Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.sCertype <> sCertype Or Me.nCertif <> nCertif Or mdtmEffecdate <> dEffecdate Or bFind Then

            '+Definición de parámetros para stored procedure 'rearrCurren_pol_tmp'
            lrecreaCurren_pol_tmp = New eRemoteDB.Execute
            With lrecreaCurren_pol_tmp
                .StoredProcedure = "reaCurren_pol_tmp"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    lblnFound = True
                    ReDim arrCurren_pol(20)
                    lintCount = 0
                    Do While Not .EOF
                        With arrCurren_pol(lintCount)
                            .Certype = sCertype
                            .nBranch = nBranch
                            .nProduct = nProduct
                            .nPolicy = nPolicy
                            .nCertif = nCertif
                            .nCurrency = lrecreaCurren_pol_tmp.FieldToClass("nCurrency")
                            .dEffecdate = dEffecdate
                            .dNulldate = dNulldate
                            .nExchange = lrecreaCurren_pol_tmp.FieldToClass("nExchange")
                            .sDescrip = lrecreaCurren_pol_tmp.FieldToClass("sDescript")
                            mblnChargeArr = True
                        End With
                        .RNext()
                        lintCount = lintCount + 1
                    Loop
                    .RCloseRec()
                    ReDim Preserve arrCurren_pol(lintCount - 1)
                Else
                    If nCertif <> 0 Then
                        .StoredProcedure = "reaCurren_pol_tmp"
                        .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        If .Run Then
                            lblnFound = True
                            ReDim arrCurren_pol(20)
                            lintCount = 0
                            Do While Not .EOF
                                With arrCurren_pol(lintCount)
                                    .Certype = sCertype
                                    .nBranch = nBranch
                                    .nProduct = nProduct
                                    .nPolicy = nPolicy
                                    .nCertif = nCertif
                                    .nCurrency = lrecreaCurren_pol_tmp.FieldToClass("nCurrency")
                                    .dEffecdate = dEffecdate
                                    .dNulldate = dNulldate
                                    .nExchange = lrecreaCurren_pol_tmp.FieldToClass("nExchange")
                                    .sDescrip = lrecreaCurren_pol_tmp.FieldToClass("sDescript")
                                    mblnChargeArr = True
                                End With
                                .RNext()
                                lintCount = lintCount + 1
                            Loop
                            .RCloseRec()
                            ReDim Preserve arrCurren_pol(lintCount - 1)
                            Me.nBranch = nBranch
                            Me.nProduct = nProduct
                            Me.dEffecdate = dEffecdate
                            Call Maching_Cur()
                        End If
                    End If
                End If
                If lblnFound Then
                    Me.nPolicy = nPolicy
                    Me.nBranch = nBranch
                    Me.nProduct = nProduct
                    Me.sCertype = sCertype
                    Me.nCertif = nCertif
                    mdtmEffecdate = dEffecdate
                End If
            End With
        Else
            lblnFound = True
        End If
        Find = lblnFound

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0

        'UPGRADE_NOTE: Object lrecreaCurren_pol_tmp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCurren_pol_tmp = Nothing
    End Function
	
	'%Charge_Combo: Función que carga las monedas de una poliza a un combo empleando el
	'%arreglo de la clase...
	Public Function Charge_Combo() As String
		
		Dim lintIndex As Integer
		Dim lstrColon As String
		
		Charge_Combo = String.Empty
		lstrColon = String.Empty
		
		'+Si el arreglo de la clase contiene informacion se carga la cadena para el combo
		
		If mblnChargeArr Then
			lintIndex = 0
			Do While lintIndex <= UBound(arrCurren_pol)
				Charge_Combo = Trim(Charge_Combo) & lstrColon & CStr(arrCurren_pol(lintIndex).nCurrency)
				lintIndex = lintIndex + 1
				lstrColon = ","
			Loop 
		End If
	End Function
	
	'%Val_Curren_pol: Función que busca una información de una moneda en el arreglo de la clase dado
	'%un indice de busqueda...
	Public Function Val_Curren_pol(ByVal intIndex As Integer) As Boolean
		'+Si el arreglo de la clase contiene informacion se carga el combo
		If mblnChargeArr Then
			If intIndex <= UBound(arrCurren_pol) Then
				With arrCurren_pol(intIndex)
					nCurrency = .nCurrency
					sDescript = .sDescrip
					nExchange = .nExchange
					sCertype = .Certype
					nBranch = .nBranch
					nProduct = .nProduct
					nPolicy = .nPolicy
					nCertif = .nCertif
					dEffecdate = .dEffecdate
					dNulldate = .dNulldate
					nExist = .nExist
				End With
				Val_Curren_pol = True
			End If
		End If
	End Function
	
	'%Delete_History: Este método se encarga de eliminar registros en la tabla "Curren_pol". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete_History() As Boolean
		Dim lintCount As Integer
		Dim lrecinsDelCurren_pol As eRemoteDB.Execute
		
		On Error GoTo insDelcurren_pol_Err
		
		lrecinsDelCurren_pol = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.insDelCurren_pol'
		'+Información leída el 17/11/1999 03:22:17 PM
		
		With lrecinsDelCurren_pol
			.StoredProcedure = "insDelCurren_pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lintCount = 0
				While arrCurren_pol(lintCount).nCurrency <> nCurrency
					lintCount = lintCount + 1
				End While
				Do While lintCount < UBound(arrCurren_pol)
					With arrCurren_pol(lintCount)
						.Certype = arrCurren_pol(lintCount + 1).Certype
						.nBranch = arrCurren_pol(lintCount + 1).nBranch
						.nProduct = arrCurren_pol(lintCount + 1).nProduct
						.nPolicy = arrCurren_pol(lintCount + 1).nPolicy
						.nCertif = arrCurren_pol(lintCount + 1).nCertif
						.nCurrency = arrCurren_pol(lintCount + 1).nCurrency
						.dEffecdate = arrCurren_pol(lintCount + 1).dEffecdate
						.dNulldate = arrCurren_pol(lintCount + 1).dNulldate
					End With
				Loop 
				If UBound(arrCurren_pol) = 0 Then
					With arrCurren_pol(lintCount)
						.Certype = CStr(0)
						.nBranch = 0
						.nProduct = 0
						.nPolicy = 0
						.nCertif = 0
						.nCurrency = 0
						.dEffecdate = System.Date.FromOADate(0)
						.dNulldate = System.Date.FromOADate(0)
					End With
				Else
					ReDim Preserve arrCurren_pol(UBound(arrCurren_pol) - 1)
				End If
				.RCloseRec()
			End If
		End With
		
insDelcurren_pol_Err: 
		If Err.Number Then
			Delete_History = False
		End If
		
		'UPGRADE_NOTE: Object lrecinsDelCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDelCurren_pol = Nothing
		On Error GoTo 0
		
	End Function
	
	'%valInitCurren: funcion que inserta en la tabla curren_pol (como valor inicial) la moneda que aparazca por defecto en la tabla
	'%cur_allow....
	Public Function valInitCurren(ByVal llngBranch As Integer, ByVal llngProduct As Integer, ByVal llngPolicy As Double, ByVal llngCertif As Double, ByVal lstrCertype As String, ByVal ldtmEffecdate As Date) As Boolean
		Dim lintCount As Integer
		
		valInitCurren = False
		If lclsCur_allow.Find(llngBranch, llngProduct, ldtmEffecdate) Then
			lintCount = 0
			mstrImage = eFunctions.Sequence.etypeImageSequence.eRequired
			mblnAutoFill = False
			Do While lclsCur_allow.Val_Cur_Allow(lintCount)
				If CShort(lclsCur_allow.sDefaulti) = 1 Then
					nBranch = llngBranch
					nProduct = llngProduct
					nPolicy = llngPolicy
					nCurrency = lclsCur_allow.nCurrency
					sDefaulti = CInt(lclsCur_allow.sDefaulti)
					sCertype = lstrCertype
					nCertif = llngCertif
					dEffecdate = ldtmEffecdate
					nExchange = lclsCur_allow.nExchange
					sDescript = lclsCur_allow.sDescript
					valInitCurren = Add
					mstrImage = eFunctions.Sequence.etypeImageSequence.eOK
					'+Se coloca en true la variable que indica si se cargaron las monedas automaticamente
					mblnAutoFill = True
				End If
				lintCount = lintCount + 1
			Loop 
		End If
		
	End Function
	
	'%valCurLocal: funcion que valida si se esta trabajando con la moneda local, es decir, si la
	'%moneda local esta definida para el producto
	Public Function valCurLocal(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lintIndex As Integer
		
		mblnAutoFill = False
		With lclsCur_allow
			If .Find(nBranch, nProduct, dEffecdate) Then
				lintIndex = 0
				Do While .Val_Cur_Allow(lintIndex)
					If Not mblnAutoFill Then
						If CDbl(.sDefaulti) = 1 Then
							mblnAutoFill = True
						End If
					End If
					If .nCurrency = 1 Then
						valCurLocal = True
					End If
					lintIndex = lintIndex + 1
				Loop 
			End If
		End With
		'+Si no se cargaron automaticamente las monedas la ventana se coloca como requerida
		mstrImage = IIf(mblnAutoFill, eFunctions.Sequence.etypeImageSequence.eOK, eFunctions.Sequence.etypeImageSequence.eRequired)
	End Function
	
	'%Maching_Cur: Esta función realiza un manejo particular de las monedas en el caso de los certificados realizando un maching
	'%entre las monedas de la poliza matriz y las monedas especificadas para el producto...
	Private Function Maching_Cur() As Boolean
		Dim lintIndex As Integer
		Dim lintCount As Integer
		Dim lblnMaching As Boolean
		
		lintIndex = 0
		With lclsCur_allow
			If .Find(nBranch, nProduct, dEffecdate) Then
				Do While .Val_Cur_Allow(lintIndex)
					lintCount = 0
					lblnMaching = False
					Do While lintCount <= UBound(arrCurren_pol)
						If arrCurren_pol(lintCount).nCurrency = .nCurrency Then
							lblnMaching = True
							Exit Do
						End If
						lintCount = lintCount + 1
					Loop 
					If Not lblnMaching Then
						If .delItem_Array(lintIndex) Then
						End If
					Else
						lintIndex = lintIndex + 1
					End If
				Loop 
			End If
		End With
		
	End Function
	
	'%Class_Initialize: Controla la creación de una instancia de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'+Se inicializa la variable que indica si el arreglo tiene informacion...
		mblnChargeArr = False
		mdtmEffecdate = eRemoteDB.Constants.dtmNull
		nCountCurPol = -1
		lclsCur_allow = New eProduct.Cur_Allow
		ReDim arrCurren_pol(0)
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Controla la destrucción de una instancia de la clase
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object lclsCur_allow may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCur_allow = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub

    '%UpdCurren_pol: En esta rutina se realiza la asiganación de los valores del
    '%frame activo a los parametros correspondientes del store-procedure que
    '%realiza la eliminacion de la historia en la estructura 'Curren_pol'.
    Public Sub updCurren_pol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCurrency As Integer, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal nUsercode As Integer = 0)
        Dim lclsCurren_pol As ePolicy.Curren_pol

        lclsCurren_pol = New ePolicy.Curren_pol

        '+Asignación de los campos claves
        Me.sCertype = sCertype
        Me.nBranch = nBranch
        Me.nProduct = nProduct
        Me.nPolicy = nPolicy
        Me.nCertif = nCertif
        Me.nCurrency = nCurrency
        Me.dEffecdate = dEffecdate
        Me.nUsercode = nUsercode
        Update()
        'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCurren_pol = Nothing

    End Sub

    '%insCurren_pol: En esta rutina se realiza la asiganación de los valores del
    '%frame activo a los parametros correspondientes del stored-procedure que
    '%realiza el mantenimiento de la historia en la estructura 'Curren_pol'.
    Public Sub CreCurren_pol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCurrency As Integer, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer)
        Dim llngCount As Integer
        Dim lclsCurren_pol As ePolicy.Curren_pol
        Dim lclsCur_allow As eProduct.Cur_Allow
        Dim lclsPolicyWin As ePolicy.Policy_Win
        lclsPolicyWin = New ePolicy.Policy_Win
        lclsCurren_pol = New ePolicy.Curren_pol
        lclsCur_allow = New eProduct.Cur_Allow

        '+Asignación de los campos claves
        Me.sCertype = sCertype
        Me.nBranch = nBranch
        Me.nProduct = nProduct
        Me.nPolicy = nPolicy
        Me.nCertif = nCertif
        Me.dEffecdate = dEffecdate
        Me.nCurrency = nCurrency
        Me.nUsercode = nUsercode

        llngCount = 0

        If lclsCur_allow.Find(nBranch, nProduct, dEffecdate) Then
            While lclsCur_allow.Val_Cur_Allow(llngCount)
                If lclsCur_allow.nCurrency = Me.nCurrency Then
                    Me.sDescript = lclsCur_allow.sDescript
                    Me.nExchange = lclsCur_allow.nExchange
                End If
                llngCount = llngCount + 1
            End While
            Add()
        End If

        Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA041", "2")

        'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCurren_pol = Nothing

        'UPGRADE_NOTE: Object lclsCur_allow may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCur_allow = Nothing
        'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicyWin = Nothing

    End Sub

    '%DelCurren_pol: realiza la eliminacion de la historia en la estructura 'Curren_pol'.
    Public Sub DelCurren_pol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCurrency As Integer, ByVal nCertif As Double, ByVal dEffecdate As Date)
        Dim lclsCurren_pol As ePolicy.Curren_pol

        lclsCurren_pol = New ePolicy.Curren_pol

        '+Asignación de los campos claves
        Me.sCertype = sCertype
        Me.nBranch = nBranch
        Me.nProduct = nProduct
        Me.nPolicy = nPolicy
        Me.nCertif = nCertif
        Me.nCurrency = nCurrency
        Delete()

        'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCurren_pol = Nothing
    End Sub

    '%insDelCurren_pol: En esta rutina se realiza la asignación de los valores del
    '%frame activo a los parametros correspondientes del store-procedure que
    '%realiza la eliminacion de la historia en la estructura 'Curren_pol'.
    Public Sub insDelCurren_pol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCurrency As Integer, ByVal nCertif As Double, ByVal dEffecdate As Date)
        Dim lclsCurren_pol As ePolicy.Curren_pol

        lclsCurren_pol = New ePolicy.Curren_pol

        '+Asignación de los campos claves
        Me.nBranch = nBranch
        Me.nPolicy = nPolicy
        Me.nProduct = nProduct
        Me.sCertype = sCertype
        Me.nCertif = nCertif
        Me.dEffecdate = dEffecdate
        Me.nCurrency = nCurrency
        Delete_History()

        'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCurren_pol = Nothing

    End Sub

    '%reaCurrency: Busca en que moneda está emitida la póliza y de tratarse de se multimoneda
    '%se devuelve "**"
    Public Function reaCurrency(ByVal nBranch As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal lblnFind As Boolean = True) As String
        Dim lrecReaCurren_pol_CAC005 As eRemoteDB.Execute
        Dim llngNumCurrency As Integer
        Dim llngCurrency As Integer
        Dim ldefList As eRemoteDB.Query

        On Error GoTo reaCurrency_err

        ldefList = New eRemoteDB.Query
        lrecReaCurren_pol_CAC005 = New eRemoteDB.Execute

        With lrecReaCurren_pol_CAC005
            .StoredProcedure = "reaCurren_pol_CAC005"

            .Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                llngNumCurrency = 0

                Do While Not (.EOF)
                    llngNumCurrency = llngNumCurrency + 1
                    llngCurrency = .FieldToClass("nCurrency")
                    .RNext()
                Loop

                If llngNumCurrency > 1 Then
                    reaCurrency = "**"
                Else
                    With ldefList
                        If .OpenQuery("Table11", "sDescript", "nCodigInt = " & CStr(llngCurrency)) Then
                            reaCurrency = .FieldToClass("sDescript")
                            .CloseQuery()
                        End If
                    End With
                End If

                .RCloseRec()
            Else
                reaCurrency = ""
            End If
        End With

        'UPGRADE_NOTE: Object lrecReaCurren_pol_CAC005 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaCurren_pol_CAC005 = Nothing
        'UPGRADE_NOTE: Object ldefList may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        ldefList = Nothing

reaCurrency_err:
        If Err.Number Then
            reaCurrency = CStr(False)
        End If

        On Error GoTo 0
    End Function
	
	'%LoadCurrency: Devuelve las monedas asociadas al producto y las monedas ingresadas
	'%en la tabla Curren_pol
    Public Function LoadCurrency(ByVal nPolicy As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sCertype As String, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False, Optional ByVal nAction As Integer = 0) As Boolean
        Dim lintCount As Integer
        Dim lrecreaCurren_pol_ca041 As eRemoteDB.Execute

        On Error GoTo reaCurren_pol_ca041_Err

        LoadCurrency = False

        If Me.nPolicy <> nPolicy Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.sCertype <> sCertype Or Me.nCertif <> nCertif Or mdtmEffecdate <> dEffecdate Or bFind Then

            '+Definición de parámetros para stored procedure 'rearrCurren_pol_tmp'
            lrecreaCurren_pol_ca041 = New eRemoteDB.Execute
            With lrecreaCurren_pol_ca041
                If nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
                    .StoredProcedure = "reaCurren_pol_tmp"
                Else
                    .StoredProcedure = "reacurren_pol_ca041"
                End If
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    LoadCurrency = True
                    ReDim arrCurren_pol(20)
                    lintCount = 0
                    Do While Not .EOF
                        With arrCurren_pol(lintCount)
                            .nCurrency = lrecreaCurren_pol_ca041.FieldToClass("nCurrency")
                            .nExchange = lrecreaCurren_pol_ca041.FieldToClass("nExchange")
                            .sDescrip = lrecreaCurren_pol_ca041.FieldToClass("sDescript")
                            If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
                                .sDefaulti = lrecreaCurren_pol_ca041.FieldToClass("sDefaulti")
                                .nExist = lrecreaCurren_pol_ca041.FieldToClass("nExist")
                            End If
                            mblnChargeArr = True
                        End With
                        .RNext()
                        lintCount = lintCount + 1
                    Loop
                    .RCloseRec()
                    ReDim Preserve arrCurren_pol(lintCount - 1)
                End If
                If LoadCurrency Then
                    Me.nPolicy = nPolicy
                    Me.nBranch = nBranch
                    Me.nProduct = nProduct
                    Me.sCertype = sCertype
                    Me.nCertif = nCertif
                    mdtmEffecdate = dEffecdate
                End If
            End With
        Else
            LoadCurrency = True
        End If

reaCurren_pol_ca041_Err:
        If Err.Number Then
            LoadCurrency = False
        End If

        'UPGRADE_NOTE: Object lrecreaCurren_pol_ca041 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCurren_pol_ca041 = Nothing
        On Error GoTo 0

    End Function
	
	'%Count_Curren_pol: Verifica si existen registros en Curren_pol
	Public Function Count_Curren_pol(ByVal nPolicy As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sCertype As String, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecvalCurren_pol As eRemoteDB.Execute
		
		On Error GoTo valCurren_pol_Err
		
		lrecvalCurren_pol = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure valCurren_pol al 08-16-2002 10:18:17
		'+
		With lrecvalCurren_pol
			.StoredProcedure = "valCurren_pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Count_Curren_pol = True
				Me.nCount = .FieldToClass("lCount")
			Else
				Count_Curren_pol = False
			End If
		End With
		
valCurren_pol_Err: 
		If Err.Number Then
			Count_Curren_pol = False
		End If
		
		'UPGRADE_NOTE: Object lrecvalCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalCurren_pol = Nothing
		On Error GoTo 0
		
	End Function
	
	'% Find_Currency_Sel: Permite obtener cantidad de monedas y la maxima moneda
	Public Function Find_Currency_Sel(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecvalCurren_pol As eRemoteDB.Execute
		
		On Error GoTo Find_Currency_Sel_Err
		
		lrecvalCurren_pol = New eRemoteDB.Execute
		
		With lrecvalCurren_pol
			.StoredProcedure = "ReaCurrency_Sel"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", nCount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Me.nCount = .Parameters("nCount").Value
				Me.nCurrency = .Parameters("nCurrency").Value
				Find_Currency_Sel = True
			End If
			
		End With
		
Find_Currency_Sel_Err: 
		If Err.Number Then
			Find_Currency_Sel = False
		End If
		
		'UPGRADE_NOTE: Object lrecvalCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalCurren_pol = Nothing
		On Error GoTo 0
		
	End Function
	
	'**%Objective: Searches to know in which currency the policy is issued in and if it is about multicurrency
	'**%           it returns "*"
	'**%Parameters:
	'**%  sCertype   - Type or Record. Sole values:     1-  Proposal     2 - Policy     3 - Quotation
	'**%  nBranch    - Code of the Line of Business. The possible values as per table 10.
	'**%  nProduct   - Code of the product.
	'**%  nPolicy    - Number identifying the policy/ quotation/ proposal
	'**%  nCertif    - Number identifying the Certificate
	'**%  dEffecdate - Date which from the record is valid.
	'%Objetivo: Busca en que moneda está emitida la póliza y de tratarse de se multimoneda
	'%          se devuelve "*"
	'%Parámetros:
	'%    sCertype   - Tipo de registro. Valores únicos:    1 - Solicitud    2 - Póliza    3 - Cotización
	'%    nBranch    - Código del ramo comercial. Valores posibles según tabla 10.
	'%    nProduct   - Código del producto.
	'%    nPolicy    - Número identificativo de la póliza/ cotización/ solicitud
	'%    nCertif    - Número identificativo del certificado
	'%    dEffecdate - Fecha de efecto del registro.
	Public Function findCurrency(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As String
		Dim lrecCurren_pol As eRemoteDB.Execute
		Dim llngCount As Integer
		
		On Error GoTo ErrorHandler
		
		lrecCurren_pol = New eRemoteDB.Execute
		
		With lrecCurren_pol
			.StoredProcedure = "reaCurren_pol_tmp"
			
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				
				'+Se carga el primer registro encontrado
				If Not .EOF Then
					Me.nCurrency = .FieldToClass("nCurrency")
					Me.sDescript = Trim(.FieldToClass("sDescript"))
				End If
				
				'+Se determina si es multimoneda
				llngCount = 0
				While Not (.EOF) And llngCount < 2
					llngCount = llngCount + 1
					.RNext()
				End While
				
				'+Se determina descripcion a retornar segun si es multimoneda o no
				If llngCount > 1 Then
					findCurrency = "*"
				Else
					findCurrency = Me.sDescript
				End If
				
				.RCloseRec()
			Else
				findCurrency = String.Empty
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCurren_pol = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCurren_pol = Nothing
		
	End Function
End Class






