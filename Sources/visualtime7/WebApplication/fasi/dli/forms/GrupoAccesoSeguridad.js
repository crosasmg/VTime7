﻿var GrupoAccesoSeguridadSupport=new function(){this.currentRow={};this.extImagen=[".jpg",".jpeg",".gif",".png",".tiff",".tif",".bmp"];this.InputToObject=function(){return{InstanceFormId:$("#GrupoAccesoSeguridadFormId").val(),Grupo_Acceso_Grid_Grupo_Acceso_Item:generalSupport.NormalizeProperties($("#Grupo_Acceso_GridTbl").bootstrapTable("getData"),"CreationDate,UpdateDate")}};this.ObjectToInput=function(n,t){t="Initialization";$("#GrupoAccesoSeguridadFormId").val(n.InstanceFormId);GrupoAccesoSeguridadSupport.LookUpForEstado_Registro(t);GrupoAccesoSeguridadSupport.Grupo_Acceso_GridTblRequest();n.Grupo_Acceso_Grid_Grupo_Acceso_Item!==null&&$("#Grupo_Acceso_GridTbl").bootstrapTable("load",n.Grupo_Acceso_Grid_Grupo_Acceso_Item)};this.ControlBehaviour=function(){new AutoNumeric("#Id_Grupo_Acceso",{decimalCharacter:generalSupport.DecimalCharacter(),digitGroupSeparator:generalSupport.DigitGroupSeparator(),maximumValue:9999999999,decimalPlaces:0,minimumValue:-9999999999});new AutoNumeric("#CreatorUserCode",{decimalCharacter:generalSupport.DecimalCharacter(),digitGroupSeparator:generalSupport.DigitGroupSeparator(),maximumValue:999999999,decimalPlaces:0,minimumValue:-999999999});new AutoNumeric("#UpdateUserCode",{decimalCharacter:generalSupport.DecimalCharacter(),digitGroupSeparator:generalSupport.DigitGroupSeparator(),maximumValue:999999999,decimalPlaces:0,minimumValue:-999999999});$("#CreationDate_group").datetimepicker({format:generalSupport.DateFormat()+" HH:mm:ss",locale:moment.locale()});generalSupport.SetCalendarPosition("#CreationDate_group");$("#UpdateDate_group").datetimepicker({format:generalSupport.DateFormat()+" HH:mm:ss",locale:moment.locale()});generalSupport.SetCalendarPosition("#UpdateDate_group")};this.ActionProcess=function(n,t){n.d.Success===!0?(n.d.Data!==null&&GrupoAccesoSeguridadSupport.ObjectToInput(n.d.Data.Instance,t),n.d.DataBehavior!==null&&generalSupport.ServerBehavior(n.d.DataBehavior)):generalSupport.NotifyFail(n.d.Reason,n.d.Code)};this.CallRenderLookUps=function(n){n.d.Success===!0&&n.d.Data.LookUps&&n.d.Data.LookUps.forEach(function(t){generalSupport.RenderLookUp(t.Key,n.d.Data.Instance[t.Key],"Initialization",t.Items,!1)})};this.Grupo_Acceso_Grid_insert=function(n,t){var i,f=Ladda.create(document.querySelector("#Grupo_Acceso_GridSaveBtn")),r,u;i=app.core.SyncWebMethod("/fasi/dli/forms/GrupoAccesoSeguridadActions.aspx/Grupo_Acceso_Grid1InsertCommandActionGrupo_Acceso",!1,JSON.stringify({ID_GRUPO_ACCESO1:n.Id_Grupo_Acceso,DESCRIPCION2:n.Descripcion,DESCRIPCION_CORTA3:n.Descripcion_Corta,ESTADO_REGISTRO4:n.Estado_Registro,CREATORUSERCODE5:app.user.userId,UPDATEUSERCODE7:app.user.userId}));i.d.Success===!0?($("#Grupo_Acceso_GridTbl").bootstrapTable("append",n),t.modal("hide"),this.Grupo_Acceso_GridTblRequest(),r=$.i18n.t("app.form.Grupo_Acceso_Grid_Message_Notify_insert5"),notification.toastr.success($.i18n.t("app.form.Grupo_Acceso_Grid_Title_Notify_insert5"),r)):(u=$.i18n.t("app.form.Grupo_Acceso_Grid_Message_Notify_insert6"),notification.swal.error($.i18n.t("app.form.Grupo_Acceso_Grid_Title_Notify_insert6"),u))};this.Grupo_Acceso_Grid_update=function(n,t){var i,f=Ladda.create(document.querySelector("#Grupo_Acceso_GridSaveBtn")),r,u;i=app.core.SyncWebMethod("/fasi/dli/forms/GrupoAccesoSeguridadActions.aspx/Grupo_Acceso_Grid1UpdateCommandActionGrupo_Acceso",!1,JSON.stringify({DESCRIPCION1:n.Descripcion,DESCRIPCION_CORTA2:n.Descripcion_Corta,ESTADO_REGISTRO3:n.Estado_Registro,UPDATEUSERCODE4:app.user.userId,GrupoAccesoIdGrupoAcceso6:n.Id_Grupo_Acceso}));i.d.Success===!0?($("#Grupo_Acceso_GridTbl").bootstrapTable("updateByUniqueId",{id:n.Id_Grupo_Acceso,row:n}),t.modal("hide"),r=$.i18n.t("app.form.Grupo_Acceso_Grid_Message_Notify_update4"),notification.toastr.success($.i18n.t("app.form.Grupo_Acceso_Grid_Title_Notify_update4"),r)):(u=$.i18n.t("app.form.Grupo_Acceso_Grid_Message_Notify_update5"),notification.swal.error($.i18n.t("app.form.Grupo_Acceso_Grid_Title_Notify_update5"),u))};this.Grupo_Acceso_Grid_delete=function(n){var t,u=Ladda.create(document.querySelector("#Grupo_Acceso_GridSaveBtn")),i,r;t=app.core.SyncWebMethod("/fasi/dli/forms/GrupoAccesoSeguridadActions.aspx/Grupo_Acceso_Grid1DeleteCommandActionGrupo_Acceso",!1,JSON.stringify({GrupoAccesoIdGrupoAcceso1:n.Id_Grupo_Acceso}));t.d.Success===!0?($("#Grupo_Acceso_GridTbl").bootstrapTable("remove",{field:"Id_Grupo_Acceso",values:[generalSupport.NumericValue("#Id_Grupo_Acceso",-9999999999,9999999999)]}),i=$.i18n.t("app.form.Grupo_Acceso_Grid_Message_Notify_delete4"),notification.toastr.success($.i18n.t("app.form.Grupo_Acceso_Grid_Title_Notify_delete4"),i)):(r=$.i18n.t("app.form.Grupo_Acceso_Grid_Message_Notify_delete5"),notification.toastr.error($.i18n.t("app.form.Grupo_Acceso_Grid_Title_Notify_delete5"),r))};this.ControlActions=function(){};this.ValidateSetup=function(){generalSupport.ExtendValidators();$.validator.addMethod("uniquecolumnGrupo_Acceso_GridId_Grupo_Acceso",function(n,t){return this.optional(t)||tableHelperSupport.UniqueColumnValidate($("#Grupo_Acceso_GridTbl"),1,n,t,$("#Grupo_Acceso_GridPopup"))});$("#GrupoAccesoSeguridadMainForm").validate({errorPlacement:function(n,t){var r=$(t).attr("name"),i=$("#"+r+"_validate");i.length?n.appendTo(i):n.insertAfter(t)},rules:{},messages:{}});$("#Grupo_Acceso_GridEditForm").validate().destroy();$("#Grupo_Acceso_GridEditForm").validate({rules:{Id_Grupo_Acceso:{AutoNumericMinValue:-9999999999,AutoNumericMaxValue:9999999999,uniquecolumnGrupo_Acceso_GridId_Grupo_Acceso:!0},Descripcion:{required:!0,maxlength:75},Descripcion_Corta:{required:!0,maxlength:40},Estado_Registro:{required:!0},CreationDate:{required:!0,DatePicker:!0},CreatorUserCode:{AutoNumericMinValue:-999999999,AutoNumericMaxValue:999999999,required:!0},UpdateDate:{required:!0,DatePicker:!0},UpdateUserCode:{AutoNumericMinValue:-999999999,AutoNumericMaxValue:999999999,required:!0}},messages:{Id_Grupo_Acceso:{AutoNumericMinValue:$.i18n.t("app.validation.Id_Grupo_Acceso.AutoNumericMinValue"),AutoNumericMaxValue:$.i18n.t("app.validation.Id_Grupo_Acceso.AutoNumericMaxValue"),uniquecolumnGrupo_Acceso_GridId_Grupo_Acceso:$.i18n.t("app.validation.Id_Grupo_Acceso.uniquecolumnGrupo_Acceso_GridId_Grupo_Acceso")},Descripcion:{required:$.i18n.t("app.validation.Descripcion.required"),maxlength:$.i18n.t("app.validation.Descripcion.maxlength")},Descripcion_Corta:{required:$.i18n.t("app.validation.Descripcion_Corta.required"),maxlength:$.i18n.t("app.validation.Descripcion_Corta.maxlength")},Estado_Registro:{required:$.i18n.t("app.validation.Estado_Registro.required")},CreationDate:{required:$.i18n.t("app.validation.CreationDate.required"),DatePicker:$.i18n.t("app.validation.CreationDate.DatePicker")},CreatorUserCode:{AutoNumericMinValue:$.i18n.t("app.validation.CreatorUserCode.AutoNumericMinValue"),AutoNumericMaxValue:$.i18n.t("app.validation.CreatorUserCode.AutoNumericMaxValue"),required:$.i18n.t("app.validation.CreatorUserCode.required")},UpdateDate:{required:$.i18n.t("app.validation.UpdateDate.required"),DatePicker:$.i18n.t("app.validation.UpdateDate.DatePicker")},UpdateUserCode:{AutoNumericMinValue:$.i18n.t("app.validation.UpdateUserCode.AutoNumericMinValue"),AutoNumericMaxValue:$.i18n.t("app.validation.UpdateUserCode.AutoNumericMaxValue"),required:$.i18n.t("app.validation.UpdateUserCode.required")}}})};this.LookUpForEstado_RegistroFormatter=function(n){return n===0||n===""?"":$("#Estado_Registro>option[value='"+n+"']").text()};this.LookUpForEstado_Registro=function(n,t){var i=$("#Estado_Registro");i.children().length===0?(i.children().remove(),i.append($("<option />").val("0").text(" Cargando...")),app.core.AsyncWebMethod("/fasi/dli/forms/GrupoAccesoSeguridadActions.aspx/LookUpForEstado_Registro",!1,JSON.stringify({id:$("#GrupoAccesoSeguridadFormId").val()}),function(r){i.children().remove();$.each(r.d.Data,function(){i.append($("<option />").val(this.Code).text(this.Description))});n!==null?i.val(n):i.val(0);t!=="Initialization"&&i.change()})):typeof n!="undefined"&&n!==null&&n.toString()!==(i.val()||"0").toString()&&(i.val(n),t!=="Initialization"&&i.change())};this.Grupo_Acceso_GridTblSetup=function(n){GrupoAccesoSeguridadSupport.LookUpForEstado_Registro("");n.bootstrapTable({maintainSelected:!0,locale:generalSupport.LanguageName()+"-CR",pagination:!0,pageSize:20,uniqueId:"Id_Grupo_Acceso",sortable:!0,sidePagination:"client",search:!0,showColumns:!0,showExport:!0,exportDataType:"all",exportOptions:{onCellHtmlData:function(n,t,i,r){var u="",f;return r!=""&&(f=$.parseHTML(r),$.each(f,function(){typeof $(this).html()=="undefined"?u+=$(this).text():typeof $(this).attr("class")=="undefined"||$(this).hasClass("th-inner")===!0?u+=$(this).html():$(this).hasClass("update edit")===!0?u+=$(this).html():(typeof $(this).attr("class")=="undefined"||$(this).hasClass("row-fluid")===!0)&&this.children.length!==0&&$.each(this.children,function(){($(this).attr("class")==="undefined"||$(this).hasClass("control-label")===!0)&&(u+=$(this).text())})})),u},maxNestedTables:0,jspdf:{orientation:"l",unit:"mm",format:"a4",margins:{left:5,right:5,top:10,bottom:10},split:10,autotable:{styles:{fontSize:9,fillColor:255,fontStyle:"normal",overflow:"linebreak",cellWidth:"auto"}}}},exportTypes:["json","xml","csv","txt","pdf","xlsx"],toolbar:"#Grupo_Acceso_Gridtoolbar",columns:[{field:"selected",checkbox:!0,formatter:"GrupoAccesoSeguridadSupport.selected_Formatter"},{field:"Id_Grupo_Acceso",title:$.i18n.t("app.form.Grupo_Acceso_GridTbl_Id_Grupo_Acceso_Title"),formatter:"GrupoAccesoSeguridadSupport.Id_Grupo_Acceso_FormatterMaskData",sortable:!0,halign:"center",visible:!1},{field:"Descripcion",title:$.i18n.t("app.form.Grupo_Acceso_GridTbl_Descripcion_Title"),events:"Grupo_Acceso_GridActionEvents",formatter:"tableHelperSupport.EditCommandFormatter",sortable:!0,halign:"center"},{field:"Descripcion_Corta",title:$.i18n.t("app.form.Grupo_Acceso_GridTbl_Descripcion_Corta_Title"),sortable:!0,halign:"center"},{field:"Estado_Registro",title:$.i18n.t("app.form.Grupo_Acceso_GridTbl_Estado_Registro_Title"),formatter:"GrupoAccesoSeguridadSupport.LookUpForEstado_RegistroFormatter",sortable:!0,halign:"center"},{field:"CreationDate",title:$.i18n.t("app.form.Grupo_Acceso_GridTbl_CreationDate_Title"),formatter:"tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds",sortable:!0,halign:"center",align:"center",visible:!1},{field:"CreatorUserCode",title:$.i18n.t("app.form.Grupo_Acceso_GridTbl_CreatorUserCode_Title"),formatter:"GrupoAccesoSeguridadSupport.CreatorUserCode_FormatterMaskData",sortable:!0,halign:"center",visible:!1},{field:"UpdateDate",title:$.i18n.t("app.form.Grupo_Acceso_GridTbl_UpdateDate_Title"),formatter:"tableHelperSupport.OnlyDateFormatterWithHoursMinutesSeconds",sortable:!0,halign:"center",align:"center",visible:!1},{field:"UpdateUserCode",title:$.i18n.t("app.form.Grupo_Acceso_GridTbl_UpdateUserCode_Title"),formatter:"GrupoAccesoSeguridadSupport.UpdateUserCode_FormatterMaskData",sortable:!0,halign:"center",visible:!1}]});$("#Grupo_Acceso_GridTbl").on("check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table",function(){var n=$("#Grupo_Acceso_GridTbl");$("#Grupo_Acceso_GridRemoveBtn").prop("disabled",!n.bootstrapTable("getSelections").length)});$("#Grupo_Acceso_GridRemoveBtn").click(function(){notification.swal.deleteRowConfirmation(function(){var n=$.map($("#Grupo_Acceso_GridTbl").bootstrapTable("getSelections"),function(n){return GrupoAccesoSeguridadSupport.Grupo_Acceso_GridRowToInput(n),GrupoAccesoSeguridadSupport.Grupo_Acceso_Grid_delete(n,null),n.Id_Grupo_Acceso});$("#Grupo_Acceso_GridRemoveBtn").prop("disabled",!0)});event.preventDefault()});$("#Grupo_Acceso_GridCreateBtn").click(function(){var n=$("#Grupo_Acceso_GridEditForm"),t=n.validate();t.resetForm();GrupoAccesoSeguridadSupport.Grupo_Acceso_GridShowModal($("#Grupo_Acceso_GridPopup").modal({show:!1}),$(this).attr("data-modal-title"))});$("#Grupo_Acceso_GridPopup").find("#Grupo_Acceso_GridSaveBtn").click(function(){var i=$("#Grupo_Acceso_GridEditForm"),u=i.validate(),n,t,r;i.valid()?(n=$("#Grupo_Acceso_GridPopup"),t="Create",n.data("id")&&(t="Update"),r=$("#Grupo_Acceso_GridSaveBtn").html(),$("#Grupo_Acceso_GridSaveBtn").html("Procesando..."),$("#Grupo_Acceso_GridSaveBtn").prop("disabled",!0),GrupoAccesoSeguridadSupport.currentRow.Id_Grupo_Acceso=generalSupport.NumericValue("#Id_Grupo_Acceso",-9999999999,9999999999),GrupoAccesoSeguridadSupport.currentRow.Descripcion=$("#Descripcion").val(),GrupoAccesoSeguridadSupport.currentRow.Descripcion_Corta=$("#Descripcion_Corta").val(),GrupoAccesoSeguridadSupport.currentRow.Estado_Registro=$("#Estado_Registro").val(),GrupoAccesoSeguridadSupport.currentRow.CreationDate=generalSupport.DatePickerValue("#CreationDate")+" HH:mm:ss",GrupoAccesoSeguridadSupport.currentRow.CreatorUserCode=generalSupport.NumericValue("#CreatorUserCode",-999999999,999999999),GrupoAccesoSeguridadSupport.currentRow.UpdateDate=generalSupport.DatePickerValue("#UpdateDate")+" HH:mm:ss",GrupoAccesoSeguridadSupport.currentRow.UpdateUserCode=generalSupport.NumericValue("#UpdateUserCode",-999999999,999999999),$("#Grupo_Acceso_GridSaveBtn").prop("disabled",!1),$("#Grupo_Acceso_GridSaveBtn").html(r),t==="Update"?GrupoAccesoSeguridadSupport.Grupo_Acceso_Grid_update(GrupoAccesoSeguridadSupport.currentRow,n):GrupoAccesoSeguridadSupport.Grupo_Acceso_Grid_insert(GrupoAccesoSeguridadSupport.currentRow,n)):generalSupport.NotifyErrorValidate(u)})};this.Grupo_Acceso_GridShowModal=function(n,t,i){var r=$("#Grupo_Acceso_GridEditForm"),u=r.validate();u.resetForm();i=i||{Id_Grupo_Acceso:0,Descripcion:"",Descripcion_Corta:"",Estado_Registro:"",CreationDate:null,CreatorUserCode:0,UpdateDate:null,UpdateUserCode:0};n.data("id",i.Id_Grupo_Acceso);n.find(".modal-title").text(t);GrupoAccesoSeguridadSupport.Grupo_Acceso_GridRowToInput(i);$("#Id_Grupo_Acceso").prop("disabled",!0);$("#CreationDate").prop("disabled",!0);$("#CreatorUserCode").prop("disabled",!0);$("#UpdateDate").prop("disabled",!0);$("#UpdateUserCode").prop("disabled",!0);n.appendTo("body");n.modal("show")};this.Grupo_Acceso_GridRowToInput=function(n){GrupoAccesoSeguridadSupport.currentRow=n;AutoNumeric.set("#Id_Grupo_Acceso",n.Id_Grupo_Acceso);$("#Descripcion").val(n.Descripcion);$("#Descripcion_Corta").val(n.Descripcion_Corta);GrupoAccesoSeguridadSupport.LookUpForEstado_Registro(n.Estado_Registro,"");$("#Estado_Registro").trigger("change");$("#CreationDate").val(generalSupport.ToJavaScriptDateCustom(n.CreationDate,generalSupport.DateFormat()+" HH:mm:ss"));AutoNumeric.set("#CreatorUserCode",n.CreatorUserCode);$("#UpdateDate").val(generalSupport.ToJavaScriptDateCustom(n.UpdateDate,generalSupport.DateFormat()+" HH:mm:ss"));AutoNumeric.set("#UpdateUserCode",n.UpdateUserCode)};this.Grupo_Acceso_GridTblRequest=function(){app.core.AsyncWebMethod("/fasi/dli/forms/GrupoAccesoSeguridadActions.aspx/Grupo_Acceso_GridTblDataLoad",!1,JSON.stringify({filter:""}),function(n){$("#Grupo_Acceso_GridTbl").bootstrapTable("load",n.d.Data!==null?n.d.Data:[])})};this.Id_Grupo_Acceso_FormatterMaskData=function(n){return AutoNumeric.format(n,{decimalCharacter:generalSupport.DecimalCharacter(),digitGroupSeparator:generalSupport.DigitGroupSeparator(),maximumValue:9999999999,decimalPlaces:0,minimumValue:-9999999999})};this.CreatorUserCode_FormatterMaskData=function(n){return AutoNumeric.format(n,{decimalCharacter:generalSupport.DecimalCharacter(),digitGroupSeparator:generalSupport.DigitGroupSeparator(),maximumValue:999999999,decimalPlaces:0,minimumValue:-999999999})};this.UpdateUserCode_FormatterMaskData=function(n){return AutoNumeric.format(n,{decimalCharacter:generalSupport.DecimalCharacter(),digitGroupSeparator:generalSupport.DigitGroupSeparator(),maximumValue:999999999,decimalPlaces:0,minimumValue:-999999999})};this.selected_Formatter=function(n){return{disabled:$("#Grupo_Acceso_GridTbl *").prop("disabled"),checked:n}};this.Init=function(){moment.locale(app.user.languageName);generalSupport.TranslateInit("GrupoAccesoSeguridad",function(){typeof masterSupport!="undefined"&&typeof constants!="undefined"&&window.location.pathname!==constants.defaultPage&&masterSupport.setPageTitle($.i18n.t("app.title"));GrupoAccesoSeguridadSupport.ValidateSetup();GrupoAccesoSeguridadSupport.ControlBehaviour();GrupoAccesoSeguridadSupport.ControlActions();app.core.AsyncWebMethod("/fasi/dli/forms/GrupoAccesoSeguridadActions.aspx/Initialization"+(window.location.href.split("?")[1]?"?"+window.location.href.split("?")[1]:""),!1,JSON.stringify({id:$("#GrupoAccesoSeguridadFormId").val(),urlid:generalSupport.URLStringValue("id"),fromid:generalSupport.URLStringValue("fromid")}),function(n){n.d.Success===!0&&(GrupoAccesoSeguridadSupport.CallRenderLookUps(n),$("#Grupo_Acceso_GridTblPlaceHolder").replaceWith('<table id="Grupo_Acceso_GridTbl"><\/table>'),GrupoAccesoSeguridadSupport.Grupo_Acceso_GridTblSetup($("#Grupo_Acceso_GridTbl")),GrupoAccesoSeguridadSupport.Grupo_Acceso_GridTblRequest())})},"dli/forms")}};$(document).ready(function(){app.security.PageSetup({Pathname:window.location.pathname,roles:["Administrador"],Element:$("#GrupoAccesoSeguridadMainForm"),CallBack:GrupoAccesoSeguridadSupport.Init})});window.Grupo_Acceso_GridActionEvents={"click .update":function(n,t,i){GrupoAccesoSeguridadSupport.Grupo_Acceso_GridShowModal($("#Grupo_Acceso_GridPopup").modal({show:!1}),$(this).attr("data-modal-title"),i)}}