<script language="VB" runat="Server">
'- Estado de los proceso batch
Const batchStatusDisabled As Short = 0 'Creado / inactivo

Const batchStatusActive As Short = 1 'Activo

Const batchStatusSend As Short = 2 'Enviado a ejecucion

Const batchStatusRun As Short = 3 'En ejecución

Const batchStatusErr As Short = 4 'Terminado con errores

Const batchStatusOk As Short = 5 'Terminado sin errores


'-Area de parametros
Const batchParAreaProc As Short = 1 'Parametros del proceso masivo

Const batchParAreaRes As Short = 2 'Parametros para procesar resultados

</script>




