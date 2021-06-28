Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ControlsReBind);

$(document).ready(function () {                            
	$(".FileUploaderDNEFiles").change(FileUploaderDNEFilesChange);
	$(".ButtonSubmitFileUploadDNEFiles").click(MensajeUsuarioRecursoTemporal("Uploading resources, please wait..."));
});

function ControlsReBind() {
	$(".FileUploaderDNEFiles").bind("change", FileUploaderDNEFilesChange);
	$(".ButtonSubmitFileUploadDNEFiles").bind("click", MensajeUsuarioRecursoTemporal("Uploading resources, please wait..."));
}

function FileUploaderDNEFilesChange() {
	$(".ButtonSubmitFileUploadDNEFiles").click();
	MensajeUsuarioRecursoTemporal("Loading resource information...");
}

function MensajeUsuarioRecursoTemporal(mensaje) {
	$(".contenedorGrilla .overlayRecursos .mensajeCargandoRecursos").html(mensaje);
	$(".contenedorGrilla .overlayRecursos").show();
}
