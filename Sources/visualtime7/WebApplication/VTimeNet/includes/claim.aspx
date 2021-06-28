<script language="VB" runat="Server">

'+ - Se definen las constantes globales para el manejo de las opciones del menú de acciones

Const clngClaimIssue As String = "1" '+ Emision de Siniestro
Const clngRecovery As String = "2" '+ Recobro de siniestro
Const clngApproval As String = "3" '+ Aprobación
Const clngClaimAmendment As String = "4" '+ Modificar siniestro
Const clngClaimQuery As String = "5" '+ Consultar Siniestro
Const clngClaimRecovery As String = "6" '+ Recuperar siniestro
Const clngClaimCancellation As String = "7" '+ Cancelar Siniestro
Const clngClaimRever As String = "8" '+ Reverso  de Siniestro
Const clngClaimPayme As String = "9" '+ Registro de Pago
Const clngPaymeQuery As String = "10" '+ Consulta de Pago
Const clngClaimRelease As String = "11" '+ Finiquito
Const clngRequeDoc As String = "12" '+ Recaudos
Const clngServiceProf As String = "13" '+ Servicios Profesionales
Const clngLetterReq As String = "14" '+ Carta Aval
Const clngClaimRejection As String = "15" '+ Rechazo de Siniestros
Const clngClaimReopening As String = "16" '+ Reapertura de Siniestros
Const clngCaratula As String = "17" '+ Reapertura de Siniestros

'+ - Se definen las constantes globales para el manejo
'+ - de Casos de Siniestros

Const clngCaseAdd As Short = 1
Const clngCaseDel As Short = 2
Const clngClientCaseAdd As Short = 4
Const clngClientCaseDel As Short = 5

'+ Contantes globales para el tipo de componentes (SI019/rea)    
Const eNone As Short = 0
Const eregister As Short = 1
Const eMotor As Short = 2
Const echassis As Short = 3

'-Se define la lista enumerada que contendra los roles de siniestro (table184)

Const clngClaimRContract As Short = 1 'Contratante
Const clngClaimRInsured As Short = 2 'Asegurado
Const clngClaimRBenefic As Short = 16 'Beneficiario
Const clngClaimRThird As Short = 3 'Tercero
Const clngClaimRUsualDriver As Short = 4 'Conductor habitual
Const clngClaimRContact As Short = 5 'Contacto
Const clngClaimRContGuar As Short = 6 'Contragarante
Const clngClaimRAddInsured As Short = 7 'Asegurado adicional
Const clngClaimRBonded As Short = 8 'Afianzado
Const clngClaimRPrivHosp As Short = 9 'Clinica
Const clngClaimRWorkShop As Short = 10 'Taller
Const clngClaimRProfessional As Short = 12 'Profesional (Perito)
Const clngClaimRAgent As Short = 13 'Agente
Const clngClaimRInsuredAffected As Short = 14 'Asegurado afectado

'-Se define la lista enumerada que contendra los tipos de proveedores (Table7027)

Const clngProviderHospital As Short = 1 'Hospital
Const clngProviderGarage As Short = 2 'Taller
Const clngProviderProfessional As Short = 3 'Profesional
Const clngProviderProvider As Short = 4 'Proveedor


</script>




