<script language="VB" runat="Server">
'-Se definen las constantes globales para el manejo de las opciones del menú de acciones
    Const clngMenuNavegation As String = "200"           ' Menú de Navegación
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

Const clngActionMainMenu As String = "201" ' Menú principal

Const clngActionErrorMenu As String = "202" ' Menú de Errores

Const clngactionpreviouswindow As String = "203" ' Ventana anterior

Const clngActionGo As String = "204" ' Ir

Const clngActionBye As String = "205" ' Salir del sistema

Const clngActionByeError As String = "206" ' Salir del Sistema de Errores

Const clngActionGenQue As String = "207" ' Ir a la consulta general


    Const clngMenuActions As String = "300"              ' Menú de Acciones
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

Const clngActionAdd As String = "301" ' Registrar

Const clngActionUpdate As String = "302" ' Actualizar

Const clngActionCut As String = "303" ' Cortar

Const clngActionInput As String = "304" ' Entrar

Const clngActionModify As String = "305" ' Modificar

Const clngActionDuplicate As String = "306" ' Duplicar

Const clngActionCutTable As String = "307" ' Cortar tabla

Const clngActionCopyTable As String = "308" ' Duplicar tabla

Const clngActionCurrency As String = "309" ' Moneda

Const clngActionDuplicateProduct As String = "310" ' Duplicar Producto


Const clngAcceptdataAccept As String = "390" ' Aceptar

Const clngAcceptdataCancel As String = "391" ' Cancelar

Const clngAcceptdataFinish As String = "392" ' Finalizar

Const clngAcceptdataRefresh As String = "393" ' Ignorar Cambios


Const clngMenuInquiry = "400"              ' Menú de Consulta
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

Const clngActionQuery As String = "401" ' Consulta

Const clngActionCondition As String = "402" ' Condición

Const clngActionReview As String = "403" ' Revisar


Const clngActionFirst As String = "490" ' Primero

Const clngActionPrevious As String = "491" ' Anteriores

Const clngActionNext As String = "492" ' Próximos

Const clngActionLast As String = "493" ' Ultimo


    Const clngMenuHelp As String = "600"                 ' Menú de Ayuda
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

Const clngActionHelp As String = "601" ' Ayuda

Const clngActionAbout As String = "603" ' Acerca de...


    Const clngMenuDelimiter As String = "99"             ' Delimitador de Items de Menú
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


Const clngActionLinkSpecial As String = "700" ' Usado para los enlaces especiales



'- Constantes para identificar el tipo de nota

Const clngAdendNote As Short = 1 ' Anexos

Const clngClientNote As Short = 2 ' Notas del Cliente

Const clngBenefNote As Short = 3 ' Beneficiarios de texto libre

Const clngNoteClause As Short = 4 ' Cláusulas partic. de la póliza

Const clngPolicyNote As Short = 5 ' Notas de la póliza

Const clngClauseNote As Short = 6 ' Texto de cláusula

Const clngSuspendNote As Short = 7 ' Nota de suspención

Const clngClaimNote As Short = 8 ' Nota de Siniestros

Const clngCarDamageNote As Short = 9 ' Daños del vehículo

Const clngRenCondNote As Short = 10 ' Condiciones de renovación

Const clngArtDetNotes As Short = 11 ' Detalle de artículos

Const clngReceiptNote As Short = 12 ' Notas de recibos

Const clngFinantialNote As Short = 14 ' Contratos de Financiamiento

Const clngNoteLedUpd As Short = 16 ' Notas de las los asientos contables

Const clngRiskNote As Short = 17 ' Descripción Riesgo asegurado

Const clngCovertextNote As Short = 20 ' Texto de Cobertura

Const clngNoteProperty As Short = 21 ' Propiedades

Const clngCashBankNote As Short = 22 ' Notas de Caja y Banco.

Const clngClaimCases As Short = 23 ' Notas de los Casos de siniestros

Const clngFinancialNote As Short = 24 ' Notas de Conceptos financieros de un cliente

Const clngCarDescriptNote As Short = 25 ' Descripción del vehículo

Const clngBudgetNote As Short = 26 ' Definición de presupuestos

Const clngClinicHistor As Short = 27 ' Detalle del diagnóstico (Historia Clínica)

Const clngNoteTransp As Short = 28 ' Notas de las rutas aseguradas

Const clngNoteObsPropo As Short = 29 ' Observaciones de una propuesta

Const clngNoteEvaluac As Short = 30 ' Evaluación Restringida

Const clngProfOrdNote As Short = 31 ' Notas de Ordenes de servicios profesionales

Const clngRecDiscNote As Short = 32 ' Notas de Recargos/Descuentos por asegurado                 

</script>




