<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Checklist.aspx.cs" Inherits="CRM.Checklist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>CHECKLIST</title>
    <link rel="stylesheet" href="assets/plugins/bootstrap/css/bootstrap.min.css">
    <link rel="stylesheet" href="assets/plugins/jquery-datatable/dataTables.bootstrap4.min.css">
    <link rel="stylesheet" href="assets/css/main1.css">
    <link rel="stylesheet" href="assets/css/color_skins.css">
    <link href="assets/css/tzamora3.css" rel="stylesheet" />
    <link href="assets/css/amalia3.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.5.0/css/all.css" integrity="sha384-B4dIYHKNBt8Bc12p+WXckhzcICo0wtJAoU8YZTY5qE0Id1GSseTk6S+L3BlXeVIU" crossorigin="anonymous">
    <link rel="stylesheet" href="assets/plugins/sweetalert/sweetalert.css" />
</head>

<body class="theme-purple">
    <style>
        .radioList {
            font-size: 15px;
            padding: 20px;
        }

        input[type="radio"] {
            -ms-transform: scale(1.5); /* IE 9 */
            -webkit-transform: scale(1.5); /* Chrome, Safari, Opera */
            transform: scale(1.5);
            margin-right: 10px;
        }

        small {
            font-weight: 1000;
        }

        .w100 {
            width: 100%;
        }
    </style>
    <div class="page-loader-wrapper">
        <div class="loader">
            <div class="m-t-30">
                <img class="zmdi-hc-spin" src="assets/images/logo.svg" width="48" height="48" alt="sQuare">
            </div>
            <p>Espere un momento ...</p>
        </div>
    </div>
    <div class="container">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager2" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel runat="server" ID="UP" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="b_guardar" />
                </Triggers>
                <ContentTemplate>
                    <asp:Panel runat="server" ID="Panel1">
                        <div>
                            <div style="display: none">
                                <asp:TextBox runat="server" ID="ID_CHECKLIST" Text="1"></asp:TextBox>
                                <asp:TextBox runat="server" ID="ID_CHECKLIST_COMPLETADO"></asp:TextBox>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-horizontal text-center">
                                        <br />
                                        <div class="row clearfix">
                                            <div class="col-sm-12">
                                                <table>
                                                    <tr>
                                                        <td style="width: 30%" class="text-left">
                                                            <img src="logozamora.png" style="width: 100%; height: auto" /></td>
                                                        <td class="text-center">
                                                            <h5><b>CHECKLIST Nº 1</b></h5>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <hr />                                      
                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group text-left">
                                                    <small>NOMBRE QUIEN REALIZA LA INSPECCIÓN *</small>
                                                    <asp:TextBox runat="server" ID="pNombreInspeccion" CssClass="form-control amalia-control" placeholder="Responder..."></asp:TextBox>
                                                </div>
                                            </div>
                                               <div class="col-lg-12">
                                                <div class="form-group text-left">
                                                    <small>LUGAR O NOMBRE PROVEEDOR*</small>
                                                    <asp:TextBox runat="server" ID="pNombreProveedor" CssClass="form-control amalia-control" placeholder="Responder..."></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <h6><b>Datos Conductor <i class="fa fa-user"></i></b></h6>
                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group text-left">
                                                    <small>NOMBRE Y APELLIDO *</small>
                                                    <asp:TextBox runat="server" ID="pNombreApellido" CssClass="form-control amalia-control" placeholder="Responder..."></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group text-left">
                                                    <small>RUT*</small>
                                                    <asp:TextBox runat="server" ID="pRut" CssClass="form-control amalia-control text-center" placeholder="Responder..."></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 50%">
                                                                <small><i class=" fa fa-truck"></i>&nbsp;PATENTE CAMION*</small>
                                                                <asp:TextBox runat="server" ID="pPatenteCamion" CssClass="form-control amalia-control text-center" placeholder="Responder..."></asp:TextBox></td>
                                                            <td style="width: 50%">
                                                                <small><i class=" fa fa-pallet"></i>&nbsp;PATENTE RAMPLA*</small>
                                                                <asp:TextBox runat="server" ID="pPatenteRampla" CssClass="form-control amalia-control text-center" placeholder="Responder..."></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <h6><b>PREGUNTAS:</b></h6>
                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">CARGA SE OBSERVA BIEN DISTRIBUIDA EN PLATAFORMA?</small>
                                                    <asp:RadioButtonList runat="server" ID="preg1" Font-Bold="true" CellPadding="8" RepeatDirection="Horizontal" CssClass="text-center w100">
                                                        <asp:ListItem style="color: green;" Text="BUENO" Selected="True"></asp:ListItem>
                                                        <asp:ListItem style="color: red;" Text="MALO"></asp:ListItem>
                                                        <asp:ListItem Text="N/A"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">ESLINGAS SE OBSERVAN TENSADAS Y ASEGURADAS?</small>
                                                    <asp:RadioButtonList runat="server" ID="preg2" Font-Bold="true" CellPadding="8" RepeatDirection="Horizontal" CssClass="text-center w100">
                                                        <asp:ListItem style="color: green;" Text="BUENO" Selected="True"></asp:ListItem>
                                                        <asp:ListItem style="color: red;" Text="MALO"></asp:ListItem>
                                                        <asp:ListItem Text="N/A"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">ESLINGAS SE ENCUENTRAN CON SUS CUBRECANTOS?</small>
                                                    <asp:RadioButtonList runat="server" ID="preg3" Font-Bold="true" CellPadding="8" RepeatDirection="Horizontal" CssClass="text-center w100">
                                                        <asp:ListItem style="color: green;" Text="BUENO" Selected="True"></asp:ListItem>
                                                        <asp:ListItem style="color: red;" Text="MALO"></asp:ListItem>
                                                        <asp:ListItem Text="N/A"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">CADENAS SE OBSERVAN TENSADAS Y ASEGURADAS?</small>
                                                    <asp:RadioButtonList runat="server" ID="preg4" Font-Bold="true" CellPadding="8" RepeatDirection="Horizontal" CssClass="text-center w100">
                                                        <asp:ListItem style="color: green;" Text="BUENO" Selected="True"></asp:ListItem>
                                                        <asp:ListItem style="color: red;" Text="MALO"></asp:ListItem>
                                                        <asp:ListItem Text="N/A"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">BARANDAS SE OBSERVAN EN BUEN ESTADO Y BIEN SEGURAS?</small>
                                                    <asp:RadioButtonList runat="server" ID="preg5" Font-Bold="true" CellPadding="8" RepeatDirection="Horizontal" CssClass="text-center w100">
                                                        <asp:ListItem style="color: green;" Text="BUENO" Selected="True"></asp:ListItem>
                                                        <asp:ListItem style="color: red;" Text="MALO"></asp:ListItem>
                                                        <asp:ListItem Text="N/A"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">SE OBSERVAN ELEMENTOS SUELTOS EN LA CARGA O SOBRE LA RAMPLA QUE PUEDAN CAER?</small>
                                                    <asp:RadioButtonList runat="server" ID="preg6" Font-Bold="true" CellPadding="8" RepeatDirection="Horizontal" CssClass="text-center w100">
                                                        <asp:ListItem style="color: green;" Text="BUENO" Selected="True"></asp:ListItem>
                                                        <asp:ListItem style="color: red;" Text="MALO"></asp:ListItem>
                                                        <asp:ListItem Text="N/A"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">CUÑAS SE ENCUENTRAN INSTALADAS DE BUENA FORMA</small>
                                                    <asp:RadioButtonList runat="server" ID="preg7" Font-Bold="true" CellPadding="8" RepeatDirection="Horizontal" CssClass="text-center w100">
                                                        <asp:ListItem style="color: green;" Text="BUENO" Selected="True"></asp:ListItem>
                                                        <asp:ListItem style="color: red;" Text="MALO"></asp:ListItem>
                                                        <asp:ListItem Text="N/A"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">ESTADO DE ESLINGAS Y CHICHARRAS</small>
                                                    <asp:RadioButtonList runat="server" ID="preg8" Font-Bold="true" CellPadding="8" RepeatDirection="Horizontal" CssClass="text-center w100">
                                                        <asp:ListItem style="color: green;" Text="BUENO" Selected="True"></asp:ListItem>
                                                        <asp:ListItem style="color: red;" Text="MALO"></asp:ListItem>
                                                        <asp:ListItem Text="N/A"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">ESTADO DE CADENAS Y TRINQUETES</small>
                                                    <asp:RadioButtonList runat="server" ID="preg9" Font-Bold="true" CellPadding="8" RepeatDirection="Horizontal" CssClass="text-center w100">
                                                        <asp:ListItem style="color: green;" Text="BUENO" Selected="True"></asp:ListItem>
                                                        <asp:ListItem style="color: red;" Text="MALO"></asp:ListItem>
                                                        <asp:ListItem Text="N/A"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">ESTADO DE CUÑAS Y CUARTONES</small>
                                                    <asp:RadioButtonList runat="server" ID="preg10" Font-Bold="true" CellPadding="8" RepeatDirection="Horizontal" CssClass="text-center w100">
                                                        <asp:ListItem style="color: green;" Text="BUENO" Selected="True"></asp:ListItem>
                                                        <asp:ListItem style="color: red;" Text="MALO"></asp:ListItem>
                                                        <asp:ListItem Text="N/A"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">ESTADO DE PISO DE RAMPLA</small>
                                                    <asp:RadioButtonList runat="server" ID="preg11" Font-Bold="true" CellPadding="8" RepeatDirection="Horizontal" CssClass="text-center w100">
                                                        <asp:ListItem style="color: green;" Text="BUENO" Selected="True"></asp:ListItem>
                                                        <asp:ListItem style="color: red;" Text="MALO"></asp:ListItem>
                                                        <asp:ListItem Text="N/A"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">ESTADO DE SEGUROS DE BARANDA</small>
                                                    <asp:RadioButtonList runat="server" ID="preg12" Font-Bold="true" CellPadding="8" RepeatDirection="Horizontal" CssClass="text-center w100">
                                                        <asp:ListItem style="color: green;" Text="BUENO" Selected="True"></asp:ListItem>
                                                        <asp:ListItem style="color: red;" Text="MALO"></asp:ListItem>
                                                        <asp:ListItem Text="N/A"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">ESTADO DE CARPAS</small>
                                                    <asp:RadioButtonList runat="server" ID="preg13" Font-Bold="true" CellPadding="8" RepeatDirection="Horizontal" CssClass="text-center w100">
                                                        <asp:ListItem style="color: green;" Text="BUENO" Selected="True"></asp:ListItem>
                                                        <asp:ListItem style="color: red;" Text="MALO"></asp:ListItem>
                                                        <asp:ListItem Text="N/A"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">ESTADO DE PONCHO</small>
                                                    <asp:RadioButtonList runat="server" ID="preg14" Font-Bold="true" CellPadding="8" RepeatDirection="Horizontal" CssClass="text-center w100">
                                                        <asp:ListItem style="color: green;" Text="BUENO" Selected="True"></asp:ListItem>
                                                        <asp:ListItem style="color: red;" Text="MALO"></asp:ListItem>
                                                        <asp:ListItem Text="N/A"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">ESTADO DE CORDELES</small>
                                                    <asp:RadioButtonList runat="server" ID="preg15" Font-Bold="true" CellPadding="8" RepeatDirection="Horizontal" CssClass="text-center w100">
                                                        <asp:ListItem style="color: green;" Text="BUENO" Selected="True"></asp:ListItem>
                                                        <asp:ListItem style="color: red;" Text="MALO"></asp:ListItem>
                                                        <asp:ListItem Text="N/A"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">ESTADO DE LINEA DE VIDA</small>
                                                    <asp:RadioButtonList runat="server" ID="preg16" Font-Bold="true" CellPadding="8" RepeatDirection="Horizontal" CssClass="text-center w100">
                                                        <asp:ListItem style="color: green;" Text="BUENO" Selected="True"></asp:ListItem>
                                                        <asp:ListItem style="color: red;" Text="MALO"></asp:ListItem>
                                                        <asp:ListItem Text="N/A"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>


                                        <hr />
                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">CANTIDAD DE ESLINGAS</small>
                                                    <asp:TextBox runat="server" ID="preg17" TextMode="Number" CssClass="form-control text-center" Text="0"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">CANTIDAD DE CUARTONES O CUÑAS</small>
                                                    <asp:TextBox runat="server" ID="preg18" TextMode="Number" CssClass="form-control text-center" Text="0"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">CANTIDAD DE CADENAS</small>
                                                    <asp:TextBox runat="server" ID="preg19" TextMode="Number" CssClass="form-control  text-center" Text="0"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">CANTIDAD DE PILARES</small>
                                                    <asp:TextBox runat="server" ID="preg20" TextMode="Number" CssClass="form-control text-center" Text="0"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divSubirArchivo" runat="server" visible="false">
                                            <hr />
                                            <h6><b><i class="fa fa-camera"></i>&nbsp;ADJUNTAR IMAGENES</b></h6>
                                            <div class="row clearfix">
                                                <div class="col-sm-12">
                                                    <small>PASO 1: Seleccione una imagen o tome una foto</small>
                                                    <input id="FU_OC" style="font-size: 12px; margin-bottom: 5px;" type="file" accept="image/x-png,image/gif,image/jpeg, image/jpg" />
                                                    <hr />
                                                    <small>PASO 2: Subir foto seleccionada.</small>
                                                    <a onclick="SubirFoto();"><span class="btn btn-primary btn-block btn-sm"><i class="fa fa-upload"></i>&nbsp;SUBIR FOTO SELECCIONADA</span></a>
                                                    <label id="L_UP"></label>
                                                    <div style="display: none">
                                                        <asp:LinkButton ClientIDMode="Static" runat="server" ID="B_CARGARADJUNTOS" OnClick="B_CARGARADJUNTOS_Click"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row clearfix">
                                                <div class="table-responsive">
                                                    <h6><b><i class="fa fa-list"></i>&nbsp;LISTADO DE IMAGENES ADJUNTAS</b></h6>
                                                    <asp:GridView DataKeyNames="id_imagen, id_checklist_completado" ClientIDMode="AutoID" runat="server"
                                                        ID="G_ADJUNTOS" CssClass="table tablaprincipal table-bordered table-condensed table-sm"
                                                        OnRowCommand="G_ADJUNTOS_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                                                        Font-Size="Small">
                                                        <HeaderStyle CssClass="thead-dark" />
                                                        <Columns>
                                                            <asp:BoundField DataField="id_imagen" Visible="false" />
                                                            <asp:BoundField DataField="id_checklist_completado" Visible="false" />
                                                            <asp:BoundField DataField="nombreOriginal" HeaderText="NOMBRE IMAGEN" />
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar" OnClientClick="return confirm('Desea eliminar el registro seleccionado?');">
                                                                     <i class="fa fa-trash fa-2x text-danger"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No se encontraron resultados.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <hr />
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <small class="font-bold">OBSERVACION / COMENTARIOS (opcional)</small>
                                                    <asp:TextBox runat="server" ID="tObservacion" CssClass="form-control text-center"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix mb-5">
                                            <div class="col-lg-12 text-center">
                                                <hr />
                                                <div id="divFinalizar" runat="server" visible="false">
                                                    <asp:LinkButton ClientIDMode="AutoID" runat="server" ID="b_finalizar" CssClass="btn btn-success" OnClick="b_finalizar_Click" OnClientClick="relojito(true);"><i class="fa fa-check"></i> FINALIZAR Y CERRAR</asp:LinkButton><br />
                                                    <small>(Al finalizar no podrá seguir modificando el Checklist y se enviará por correo a los responsables).</small>
                                                    <br />
                                                </div>
                                                <hr />
                                                <asp:LinkButton ClientIDMode="AutoID" runat="server" ID="b_guardar" CssClass="btn btn-primary" OnClick="b_guardar_Click" OnClientClick="relojito(true);"><i class="fa fa-save"></i> GUARDAR CHECKLIST</asp:LinkButton><br />
                                                <small runat="server" id="mensaje">(Primero guarde el checklist para poder adjuntar imagenes).</small>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="FINAL" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-sm-12 text-center">
                                <h2>Checklist Completado con éxito, se enviaron los correos a los encargados correspondientes</h2>
                                <h4>Ya puede cerrar esta ventana</h4>
                                <hr />
                                <i class="fa fa-check text-success fa-4x"></i>
                            </div>
                        </div>

                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
    </div>
    <!-- NO TOCAR -->
    <script src="assets/bundles/libscripts.bundle.js"></script>
    <script src="assets/bundles/vendorscripts.bundle.js"></script>
    <script src="assets/bundles/mainscripts.bundle.js"></script>
    <!-- NOTIFICACIONES -->
    <script src="assets/plugins/bootstrap-notify/bootstrap-notify.js"></script>
    <!-- DIALOGS Y ALERTS -->
    <script src="assets/plugins/sweetalert/sweetalert.min.js"></script>
    <script>
        function MostrarNotificacion(mensaje, flag) {
            var color = 'bg-black';
            if (flag == 0) { color = 'bg-red' };
            if (flag == 1) { color = 'bg-green' };
            if (flag == 2) { color = 'bg-black' };
            var allowDismiss = true;

            $.notify({
                message: mensaje
            },
                {
                    type: color,
                    allow_dismiss: allowDismiss,
                    newest_on_top: true,
                    timer: 300,
                    placement: {
                        from: 'top',
                        align: 'right'
                    },
                    z_index: 9999999999,
                    animate: {
                        enter: 'animated fadeInDown',
                        exit: 'animated fadeOutUp'
                    },
                    template: '<div data-notify="container" class="alert-visible bootstrap-notify-container alert alert-dismissible {0} ' + (allowDismiss ? "p-r-35" : "") + '" role="alert">' +
                        '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                        '<span data-notify="icon"></span> ' +
                        '<span data-notify="title">{1}</span> ' +
                        '<span data-notify="message">{2}</span>' +
                        '<div class="progress" data-notify="progressbar">' +
                        '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                        '</div>' +
                        '<a href="{3}" target="{4}" data-notify="url"></a>' +
                        '</div>'
                });
        }

        function SubirFoto() {
            relojito(true);
            var data = new FormData();
            var files = $("#FU_OC").get(0).files;           
            var guid_pro = guid();
            if (files.length > 0) {
                var id_oc = document.getElementById('<%= ID_CHECKLIST_COMPLETADO.ClientID %>').value;
                data.append("UploadedImage", files[0]);
                var ajaxRequest = $.ajax({
                    type: "POST",
                    url: "SUBE_ARCHIVOS.aspx?tipo_doc=7&id_checklist_completado=" + id_oc + "&guid=" + guid_pro,
                    contentType: false,
                    processData: false,
                    data: data
                });
                ajaxRequest.done(function (xhr, textStatus) {
                    MostrarNotificacion('Foto cargada con éxito al checklist.', 1);
                    document.getElementById('B_CARGARADJUNTOS').click();
                    relojito(false);
                });
            }
            else {
                MostrarNotificacion('Error al subir archivo', 0);
                relojito(false);
            }
        }

        function guid() {
            function s4() {
                return Math.floor((1 + Math.random()) * 0x10000)
                    .toString(16)
                    .substring(1);
            }
            return s4() + s4() + s4();
        }

        function relojito(x) {
            if (x) {
                $(".page-loader-wrapper").fadeIn();
            }
            else {
                $(".page-loader-wrapper").fadeOut();
            }
        }

        function Cierrame(id_completado) {
            window.close();
        }

    </script>
</body>
</html>
