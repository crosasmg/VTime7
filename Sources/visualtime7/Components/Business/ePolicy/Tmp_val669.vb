Option Strict Off
Option Explicit On
Public Class Tmp_val669
	'%-------------------------------------------------------%'
	'% $Workfile:: Tmp_val669.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.02                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'- Definición de la tabla TMP_VAL669 tomada el 09/04/2002
	'Column_Name                                 Type      Length  Prec  Scale Nullable
	'------------------------- --------------- - -------- ------- ----- ------ --------
	Public sKey As String ' CHAR          20              Yes
	Public sCertype As String ' CHAR           1              Yes
	Public nBranch As Integer ' NUMBER        22     5      0 Yes
	Public nProduct As Integer ' NUMBER        22     5      0 Yes
	Public nPolicy As Double ' NUMBER        22    10      0 Yes
	Public nCertif As Double ' NUMBER        22    10      0 Yes
	Public dEffecdate As Date
	Public nYear As Integer ' NUMBER        22     5      0 Yes
	Public nAge_reinsu As Integer ' NUMBER        22     5      0 Yes
	Public nAmodepacum As Double ' NUMBER        22    10      2 Yes
	Public nValpolig As Double ' NUMBER        22    10      2 Yes
	Public nValsurig As Double ' NUMBER        22    10      2 Yes
	Public nProdeathig As Double ' NUMBER        22    10      2 Yes
	Public nValpolimg As Double ' NUMBER        22    10      2 Yes
	Public nValsurimg As Double ' NUMBER        22    10      2 Yes
	Public nProdeathimg As Double ' NUMBER        22    10      2 Yes
	
	'% ClearFields: se inicializa el valor de las variables de la clase
	Private Sub ClearFields()
		sKey = String.Empty
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		nYear = eRemoteDB.Constants.intNull
		nAge_reinsu = eRemoteDB.Constants.intNull
		nAmodepacum = eRemoteDB.Constants.intNull
		nValpolig = eRemoteDB.Constants.intNull
		nValsurig = eRemoteDB.Constants.intNull
		nProdeathig = eRemoteDB.Constants.intNull
		nValpolimg = eRemoteDB.Constants.intNull
		nValsurimg = eRemoteDB.Constants.intNull
		nProdeathimg = eRemoteDB.Constants.intNull
	End Sub
	
	'* Class_Initialize: se inicializa el valor de las variables de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		ClearFields()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






