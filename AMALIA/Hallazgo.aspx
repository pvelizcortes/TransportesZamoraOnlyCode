<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="Hallazgo.aspx.cs" Inherits="AMALIA.Hallazgo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BODY" runat="server">

    <style>
        .cell-letra-chica {
            text-align: left !important;
            font-size: 11px !important;
        }
    </style>
    <section class="content">
        <div class="container">
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="body block-header">
                            <div class="row">
                                <div class="col-sm-12">
                                    <ul class="breadcrumb p-l-0 p-b-0 float-right">
                                        <li class="breadcrumb-item"><a href="index.aspx"><i class="icon-home"></i></a></li>
                                        <li class="breadcrumb-item"><a href="index.aspx">Index</a></li>
                                        <li class="breadcrumb-item active">Hallazgos</li>
                                    </ul>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:UpdatePanel runat="server" ID="UP_PRINCIPAL">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="G_PRINCIPAL" />
                                            <asp:AsyncPostBackTrigger ControlID="B_NUEVO" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <!-- LISTADO -->
                                            <asp:Panel runat="server" ID="PANEL_PRINCIPAL">
                                                <div class="row clearfix">
                                                    <div class="col-sm-12">
                                                        <asp:LinkButton CssClass="btn btn-sm btn-primary" ClientIDMode="AutoID" runat="server" ID="B_NUEVO" OnClick="B_NUEVO_Click">
                                                                                <b><small>+ INGRESAR NUEVO HALLAZGO</small></b>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row clearfix">
                                                    <div class="col-sm-2">
                                                        FECHA DESDE:
                                                        <br />
                                                        <asp:TextBox runat="server" ID="FILTRA_FECHA_DESDE" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        FECHA HASTA:
                                                        <br />
                                                        <asp:TextBox runat="server" ID="FILTRA_FECHA_HASTA" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                    </div>
                                                    <%--   <div class="col-sm-2">
                                                        SOLICITANTE:
                                                        <br />
                                                        <asp:DropDownList runat="server" ID="CB_USUARIOS" CssClass="form-control"></asp:DropDownList>
                                                    </div>--%>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-sm-2">
                                                        <br />
                                                        <asp:LinkButton runat="server" ID="B_FILTRAR" ClientIDMode="AutoID" OnClick="B_FILTRAR_Click" CssClass="btn btn-primary btn-sm btn-round"><i class="fa fa-search"></i> FILTRAR</asp:LinkButton>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-sm-12 table-responsive">
                                                        <asp:GridView DataKeyNames="id_hallazgo" ClientIDMode="AutoID" runat="server" ID="G_PRINCIPAL"
                                                            CssClass="table table-bordered tablaprincipal table-hover js-exportable" Font-Size="12px" OnRowCommand="G_PRINCIPAL_RowCommand"
                                                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                            <HeaderStyle CssClass="thead-dark" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                            <i class="icon-pencil" style="font-size:20px"></i>
                                                                        </asp:LinkButton>
                                                                        &nbsp;        &nbsp;
                                                                             <asp:LinkButton ToolTip="Imprimir PDF" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Print" OnClientClick='<%# String.Format("return ImprimeOC({0});", Eval("id_hallazgo")) %>'>
                                                                   <i class="fa fa-print" style="font-size:20px"></i>
                                                                             </asp:LinkButton>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="id_hallazgo" Visible="false" />
                                                                <asp:BoundField DataField="num_referencia" HeaderText="Nº REF." ItemStyle-Font-Bold="true" />
                                                                <asp:BoundField DataField="nombre_remitente" HeaderText="REMITENTE" />
                                                                <asp:BoundField DataField="fecha_envio" HeaderText="FECHA ENVIO" DataFormatString="{0:dd/MM/yyyy}" />
                                                                <asp:BoundField DataField="area" HeaderText="AREA" />
                                                                <asp:BoundField DataField="deteccion" HeaderText="DETECCION" />
                                                                <asp:BoundField DataField="tipo" HeaderText="TIPO" />
                                                                <asp:BoundField DataField="origen" HeaderText="ORIGEN" />
                                                                <asp:BoundField DataField="destino" HeaderText="DESTINO" />
                                                                <asp:BoundField DataField="nombre_deteccion" HeaderText="NOMBRE DETECCION" />
                                                                <asp:BoundField DataField="deteccion_hallazgo" HeaderText="DET. HALLAZGO" ItemStyle-Wrap="true" />
                                                                <asp:BoundField DataField="doc_referencia" HeaderText="DOC. REF." />
                                                                <asp:BoundField DataField="accion_inmediata" HeaderText="ACCION INMEDIATA" />
                                                                <asp:BoundField DataField="estado" HeaderText="ESTADO" />
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                No se encontraron resultados.
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <!-- ENC -->
                                            <asp:Panel ID="PANEL_ENC" runat="server" Visible="false">
                                                <%-- <div runat="server" class="text-center" id="DIVAUTORIZADAS" visible="false">
                                                    <hr />
                                                    <div class="row clearfix  text-center">
                                                        <div class="col-sm-4">
                                                            <small><b>AUTORIZACION <span class="text-purple">MAURICIO ZAPATA</span></b></small><br />
                                                            <asp:Label runat="server" Font-Size="Large" Font-Bold="true" ID="LBLAUTMZ"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <small><b>AUTORIZACION <span class="text-purple">FELIPE ZAMORA</span></b></small><br />
                                                            <asp:Label runat="server" Font-Size="Large" Font-Bold="true" ID="LBLAUTFZ"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <small><b>OBSERVACION</b></small><br />
                                                            <asp:Label runat="server" ID="LBLOBSERVACION"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <asp:TextBox runat="server" CssClass="text-center" Style="width: 50%" Font-Size="Smaller" ID="T_COPYCLIPBOARD"></asp:TextBox>
                                                    <br />
                                                    <small class="text-primary">(Link de autorización)</small>
                                                </div>--%>
                                                <h4>
                                                    <div class="btn btn-primary  btn-sm" onclick="ImprimeOC2();"><i class="fa fa-print"></i>Imprimir Hallazgo</div>
                                                    &nbsp;&nbsp;&nbsp;<asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-danger btn-sm" runat="server" ID="B_ELIMINAR_OC" OnClientClick="return confirm('Desea eliminar el registro seleccionado?');" OnClick="B_BORRAHALLAZGO_Click"><i class="fa fa-times"></i>&nbsp;ELIMINAR HALLAZGO</asp:LinkButton>
                                                </h4>
                                                <hr />
                                                <form class="form-horizontal">
                                                    <div style="display: none">
                                                        <asp:TextBox runat="server" ID="T_ID"></asp:TextBox>
                                                    </div>
                                                    <div class="row clearfix">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Numero de referencia</b></small>
                                                                <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="tNumReferencia"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Fecha del envío</b></small>
                                                                <asp:TextBox runat="server" Enabled="false" TextMode="Date" CssClass="form-control" ID="tFechaEnvio"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row clearfix">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Nombre del remitente emisión</b></small>
                                                                <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="tNombreRemitente"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                &nbsp;
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <h4 class="text-purple">Identificación</h4>
                                                    <div class="row clearfix">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Area</b></small>
                                                                <asp:DropDownList runat="server" ID="CBAREA" CssClass="form-control">
                                                                    <asp:ListItem Text="Administración y Finanzas" Value="Administración y Finanzas"></asp:ListItem>
                                                                    <asp:ListItem Text="Calidad" Value="Calidad"></asp:ListItem>
                                                                    <asp:ListItem Text="Conductores" Value="Conductores"></asp:ListItem>
                                                                    <asp:ListItem Text="Coordinación" Value="Coordinación"></asp:ListItem>
                                                                    <asp:ListItem Text="Obra" Value="Obra"></asp:ListItem>
                                                                    <asp:ListItem Text="Operador Logistico" Value="Operador Logistico"></asp:ListItem>
                                                                    <asp:ListItem Text="RRHH" Value="RRHH"></asp:ListItem>
                                                                    <asp:ListItem Text="Seguridad" Value="Seguridad"></asp:ListItem>
                                                                    <asp:ListItem Text="Transportes" Value="Transportes"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Nombre quien detecta</b></small>
                                                                <asp:TextBox runat="server" ID="tNombreDetecta" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row clearfix">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Detección</b></small>
                                                                <asp:DropDownList runat="server" ID="CBDETECCION" CssClass="form-control">
                                                                    <asp:ListItem Text="Inconformidad" Value="Inconformidad"></asp:ListItem>
                                                                    <asp:ListItem Text="Observación" Value="Observación"></asp:ListItem>
                                                                    <asp:ListItem Text="Oportunidad de Mejora" Value="Oportunidad de Mejora"></asp:ListItem>
                                                                    <asp:ListItem Text="Otro" Value="Otro"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Tipo</b></small>
                                                                <asp:DropDownList runat="server" ID="CBTIPO" CssClass="form-control">
                                                                    <asp:ListItem Text="Carga Alterada" Value="Carga Alterada"></asp:ListItem>
                                                                    <asp:ListItem Text="Incumplimiento de Horarios" Value="Incumplimiento de Horarios"></asp:ListItem>
                                                                    <asp:ListItem Text="Imperfecciones en los Vehículos" Value="Imperfecciones en los Vehículos"></asp:ListItem>
                                                                    <asp:ListItem Text="Incumplimiento de Compromisos" Value="Incumplimiento de Compromisos"></asp:ListItem>
                                                                    <asp:ListItem Text="Documentación Inconforme" Value="Documentación Inconforme"></asp:ListItem>
                                                                    <asp:ListItem Text="Inventario Incompleto" Value="Inventario Incompleto"></asp:ListItem>
                                                                    <asp:ListItem Text="Error en la Inspección de Seguridad" Value="Error en la Inspección de Seguridad"></asp:ListItem>
                                                                    <asp:ListItem Text="No presenta Orden de Compra" Value="No presenta Orden de Compra"></asp:ListItem>

                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row clearfix">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Origen</b></small>
                                                                <asp:DropDownList runat="server" ID="CBORIGEN" CssClass="form-control">
                                                                    <asp:ListItem Text="Interno" Value="Interno"></asp:ListItem>
                                                                    <asp:ListItem Text="Cliente" Value="Cliente"></asp:ListItem>
                                                                    <asp:ListItem Text="Proveedor" Value="Proveedor"></asp:ListItem>
                                                                    <%--     <asp:ListItem Text="Coordinación" Value="Coordinación"></asp:ListItem>
                                                                    <asp:ListItem Text="Operador Logistico" Value="Operador Logistico"></asp:ListItem>
                                                                    <asp:ListItem Text="Transportes" Value="Transportes"></asp:ListItem>
                                                                    <asp:ListItem Text="Calidad" Value="Calidad"></asp:ListItem>
                                                                    <asp:ListItem Text="Seguridad" Value="Seguridad"></asp:ListItem>
                                                                    <asp:ListItem Text="Obra" Value="Obra"></asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Destino</b></small>
                                                                <asp:DropDownList runat="server" ID="CBDESTINO" CssClass="form-control">
                                                                    <asp:ListItem Text="-- SELECCIONE --" Value=" "></asp:ListItem>
                                                                    <asp:ListItem Text="Interno" Value="Interno"></asp:ListItem>
                                                                    <asp:ListItem Text="Cliente" Value="Cliente"></asp:ListItem>
                                                                    <asp:ListItem Text="Proveedor" Value="Proveedor"></asp:ListItem>
                                                                    <%--     <asp:ListItem Text="Coordinación" Value="Coordinación"></asp:ListItem>
                                                                    <asp:ListItem Text="Operador Logistico" Value="Operador Logistico"></asp:ListItem>
                                                                    <asp:ListItem Text="Transportes" Value="Transportes"></asp:ListItem>
                                                                    <asp:ListItem Text="Calidad" Value="Calidad"></asp:ListItem>
                                                                    <asp:ListItem Text="Seguridad" Value="Seguridad"></asp:ListItem>
                                                                    <asp:ListItem Text="Obra" Value="Obra"></asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <h4 class="text-purple">Detección de Hallazgo</h4>
                                                    <div class="row clearfix">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Detección del Hallazgo</b></small>
                                                                <asp:TextBox runat="server" ID="tDeteccionHallazgo" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Documento de referencia</b></small>
                                                                <asp:TextBox runat="server" ID="tDocReferencia" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row clearfix">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Acción Inmediata</b></small>
                                                                <asp:TextBox runat="server" ID="tAccionInmediata" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Empresa Involucrada</b></small>
                                                                <asp:TextBox runat="server" ID="tEmpInvolucrada" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <!-- DIV ADJUJNTAR -->
                                                    <div runat="server" id="DIV_ADJUNTAR" visible="false">
                                                        <h6 class="text-purple"><b><i class="fa fa-list"></i></b>Adjuntar archivos:</h6>
                                                        <div class="row  clearfix">
                                                            <div class="col-sm-6">
                                                                <input id="FU_OC" style="font-size: 12px; margin-bottom: 5px;" type="file" />
                                                                <a onclick="SubirDocumento3();"><span class="btn btn-primary btn-block btn-sm"><i class="fa fa-upload"></i>&nbsp;Subir Documento seleccionado</span></a>
                                                                <label id="L_UP3"></label>
                                                                <div style="display: none">
                                                                    <asp:TextBox runat="server" ID="T_REV_NOMBRE_BD"></asp:TextBox>
                                                                    <asp:LinkButton ClientIDMode="Static" runat="server" ID="B_CARGARADJUNTOS" OnClick="B_CARGARADJUNTOS_Click"></asp:LinkButton>
                                                                </div>

                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="table-responsive">
                                                                    <asp:GridView DataKeyNames="id_doc, id_hallazgo" ClientIDMode="AutoID" runat="server"
                                                                        ID="G_ADJUNTOS" CssClass="table tablaprincipal table-bordered table-condensed table-sm"
                                                                        OnRowCommand="G_ADJUNTOS_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                                                                        Font-Size="Small">
                                                                        <HeaderStyle CssClass="thead-dark" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="id_doc" Visible="false" />
                                                                            <asp:BoundField DataField="id_hallazgo" Visible="false" />
                                                                            <asp:BoundField DataField="nom_real" HeaderText="NOMBRE ADJUNTO" />
                                                                            <asp:BoundField DataField="fecha" HeaderText="FECHA" />
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="veradjunto" ToolTip="Ver Adjunto">
                                                                                <i class="fa fa-file fa-2x text-primary"></i>
                                                                                    </asp:LinkButton>
                                                                                    &nbsp;&nbsp;
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
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 text-right">
                                                            <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-raised btn-primary btn-round waves-effect" runat="server" ID="B_GUARDAROC" OnClick="B_GUARDAROC_Click"><i class="fa fa-save"></i>&nbsp;GUARDAR HALLAZGO</asp:LinkButton>
                                                            <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-raised btn-default btn-round waves-effect" runat="server" ID="B_VOLVER" OnClick="B_VOLVER_Click"><i class="fa fa-undo"></i>&nbsp;VOLVER</asp:LinkButton>


                                                        </div>
                                                    </div>
                                                </form>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CODIGO_JAVASCRIPT" runat="server">
    <script>
        $(document).ready(function () {
            Datatables();
        });

        function Datatables() {
            $('#<%= G_PRINCIPAL.ClientID %>').DataTable({
                destroy: true,
                "pageLength": 50,
                stateSave: true,
                dom: 'lBfrtip',
                buttons: [
                    'copy',
                    'print'
                ],
                "language": {
                    "url": "assets/Spanish.json"
                },
                columnDefs: [{
                    type: 'date-uk',
                    targets: [1]
                }]
            });
        }
        function guid() {
            function s4() {
                return Math.floor((1 + Math.random()) * 0x10000)
                    .toString(16)
                    .substring(1);
            }
            return s4() + s4() + s4();
        }

        function SubirDocumento3() {
            var data = new FormData();
            var files = $("#FU_OC").get(0).files;
            var guid_pro = guid();
            if (files.length > 0) {
                var id_hallazgo = document.getElementById('<%= T_ID.ClientID %>').value;
                data.append("UploadedImage", files[0]);
                var ajaxRequest = $.ajax({
                    type: "POST",
                    url: "SUBE_ARCHIVOS.aspx?tipo_doc=6&id_hallazgo=" + id_hallazgo + "&guid=" + guid_pro,
                    contentType: false,
                    processData: false,
                    data: data
                });
                ajaxRequest.done(function (xhr, textStatus) {
                    MostrarNotificacion('Documento cargado con éxito', 1);
                    document.getElementById('B_CARGARADJUNTOS').click();
                });
            }
            else {
                MostrarNotificacion('Error al subir archivo', 0);
            }
        }

        function VerArchivo(archivo) {
            //var win = window.open('Documentos/Camiones/MANT/' + id_camion + '/' + archivo, '_blank');
            var win = window.open(archivo, '_blank');
            win.focus();
        }

        function ImprimeOC(num_hallazgo) {
            var win = window.open('Imprime_hallazgo.aspx?num_hallazgo=' + num_hallazgo, '_blank');
            win.focus();
        }

        function ImprimeOC2() {
            var num_hallazgo = document.getElementById('<%= T_ID.ClientID %>').value;
            var win = window.open('Imprime_hallazgo.aspx?num_hallazgo=' + num_hallazgo, '_blank');
            win.focus();
        }
    </script>
</asp:Content>
