<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="FacturaCompra.aspx.cs" Inherits="AMALIA.FacturaCompra" %>

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
                            <asp:UpdatePanel runat="server" ID="UP_NUEVO" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="CB_CAMION" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <asp:DropDownList runat="server" ID="CB_CAMION" CssClass="form-control combopro"
                                                OnSelectedIndexChanged="CB_CAMION_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-9">
                                            <ul class="breadcrumb p-l-0 p-b-0 float-right">
                                                <li class="breadcrumb-item"><a href="index.aspx"><i class="icon-home"></i></a></li>
                                                <li class="breadcrumb-item"><a href="index.aspx">Index</a></li>
                                                <li class="breadcrumb-item active">Facturas de Compra</li>
                                            </ul>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:UpdatePanel runat="server" ID="UP_PRINCIPAL" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="G_PRINCIPAL" />
                                            <asp:AsyncPostBackTrigger ControlID="B_NUEVO" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div style="display: none">
                                                <a id="MODAL_FACTURA" href="#modalfactura" data-toggle="modal" data-target="#modalfactura"></a>
                                            </div>

                                            <asp:Panel runat="server" ID="PANEL_PRINCIPAL" Visible="false">
                                                <div class="body">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="card">
                                                                <div class="row">
                                                                    <div class="col-sm-12">                                                                      
                                                                        <h5>Patente: <span runat="server" id="DIV_PATENTE"></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                            <asp:LinkButton CssClass="btn btn-sm btn-primary" ClientIDMode="AutoID" runat="server" ID="B_NUEVO" OnClick="B_NUEVO_Click">
                                                                                <b><small>+ INGRESAR NUEVA COMPRA</small></b>
                                                                            </asp:LinkButton></h5>
                                                                        <div runat="server" id="ENC_CAMION">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>                                                         
                                                            <div class="row">
                                                                <div class="col-3 text-center">
                                                                    <h5 class="m-b-0 text-danger" runat="server" id="DIV_TOTAL"></h5>
                                                                    <small>Total Compras</small>
                                                                </div>
                                                                <div class="col-3 text-center">
                                                                    <h5 class="m-b-0" runat="server" id="DIV_NUM_COMPRAS"></h5>
                                                                    <small>Nº Compras</small>
                                                                </div>
                                                                <div class="col-3 text-center">
                                                                    <h5 class="m-b-0" runat="server" id="DIV_NUM_DOCS"></h5>
                                                                    <small>Nº Documentos</small>
                                                                </div>
                                                                <div class="col-3 text-center">
                                                                    <h5 class="m-b-0 text-danger" runat="server" id="DIV_SIN_PAGAR"></h5>
                                                                    <small class="text-danger"><b>Docs. Sin Pagar</b></small>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                             
                                                <div class="row" style="border-top:1px solid lightgray;margin-top:10px;">
                                                    <div class="col-sm-12 table-responsive">
                                                        <asp:GridView DataKeyNames="id_compra, estado" ClientIDMode="AutoID" runat="server" ID="G_PRINCIPAL"
                                                            CssClass="table table-bordered tablaprincipal table-hover js-exportable table-condensed" OnRowCommand="G_PRINCIPAL_RowCommand"
                                                            OnRowDataBound="G_PRINCIPAL_RowDataBound"
                                                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                            <HeaderStyle CssClass="thead-dark" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText=" " ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ToolTip="Editar / Ver" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                                <i class="fa fa-eye"></i>                                                                                
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="id_compra" Visible="false" />
                                                                <asp:BoundField DataField="id_proveedor" Visible="false" />
                                                                <asp:BoundField HeaderText="FECHA" DataField="fecha_compra" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField HeaderText="DOCUMENTO" DataField="tipo_documento" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Nº" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ToolTip="Ver Factura" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar" CssClass="text-purple">
                                                                                 <b><%# Eval("num_documento") %> </b>                                                                          
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <div style="width: 100%">
                                                                            <div id="div_estado" runat="server"></div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="PROVEEDOR" DataField="nom_proveedor" ItemStyle-CssClass="cell-letra-chica" />
                                                                <asp:BoundField HeaderText="DETALLE" DataField="detalle" ItemStyle-CssClass="cell-letra-chica" />
                                                                <asp:BoundField HeaderText="TOTAL" DataField="total" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C0}" ItemStyle-Font-Bold="true" />
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="BORRAR" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar" OnClientClick="return confirm('Desea eliminar el registro seleccionado?');">
                                                                                <b><i class="icon-trash"></i></b>
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
    <!-- MODAL CARGA DE GASTO GENERAL -->
    <div class="modal fade" id="modalfactura" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="UPMODAL" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="G_DETALLE_DOCS" />
                            <asp:AsyncPostBackTrigger ControlID="B_GUARDAR" />
                            <asp:AsyncPostBackTrigger ControlID="B_AGREGAR_URL_DOC" />
                        </Triggers>
                        <ContentTemplate>
                            <h4>
                                <b>
                                    <i class="fa fa-file-invoice"></i>&nbsp;<asp:Label runat="server" ID="LBL_ESTADO" Text="DETALLE DE LA COMPRA"></asp:Label></b>
                            </h4>
                            <hr />
                            <form class="form-horizontal">
                                <div style="display: none">
                                    <asp:TextBox runat="server" ID="T_ID"></asp:TextBox>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <small><b>TIPO DE DOCUMENTO *</b></small>
                                            <asp:DropDownList runat="server" ID="CB_TIPO_DOCUMENTO" CssClass="form-control combopro">
                                                <asp:ListItem Text="FACTURA" Value="FACTURA" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="NOTA DE CREDITO" Value="NOTA DE CREDITO"></asp:ListItem>
                                                <asp:ListItem Text="OTRO" Value="OTRO"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <small><b>Nº DOCUMENTO *</b></small>
                                            <asp:TextBox runat="server" ID="T_NUM_DOC" CssClass="form-control text-right"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <small><b>PROVEEDOR *</b> | <a href="PROVEEDORES.aspx" target="_blank">+ Crear nuevo</a></small>
                                            <asp:DropDownList runat="server" ID="CB_PROVEEDOR" CssClass="form-control combopro"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <small><b>FECHA DOCUMENTO *</b></small>
                                            <asp:TextBox runat="server" TextMode="Date" CssClass="form-control" ID="FECHA_DOCUMENTO"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <small><b>ESTADO DOCUMENTO *</b></small>
                                            <asp:DropDownList runat="server" ID="CB_ESTADO_DOC" CssClass="form-control combopro">
                                                <asp:ListItem Text="NO PAGADO" Value="NO PAGADO" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="PAGADO" Value="PAGADO"></asp:ListItem>
                                                <asp:ListItem Text="OTRO" Value="OTRO"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <small><b>OBSERVACIÓN O DETALLE DEL DOCUMENTO</b> (Opcional)</small>
                                            <asp:TextBox runat="server" ID="T_DETALLE" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <small class="text-danger"><b>TOTAL ($) *</b></small>
                                            <asp:TextBox runat="server" ID="T_TOTAL" CssClass="form-control text-right border-danger" TextMode="Number"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <span runat="server" id="mensaje_guardar"><small class="font-italic">(Para agregar documentos primero haga click en "GUARDAR")</small></span>
                                <div class="row" runat="server" id="div_cargar_docs" visible="false">

                                    <div class="col-sm-4">
                                        <h6><b><i class="fa fa-file-alt"></i>&nbsp;ADJUNTAR</b> DOCUMENTO</h6>
                                        <%-- <b>SUBIR DOCUMENTOS <i class="fa fa-upload"></i></b>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <input id="FU_LOGO" style="font-size: 10px; margin-bottom: 5px;" type="file" title="Cargar Documento" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <a onclick="SubirDocumento();"><span class="btn btn-primary btn-sm btn-block"><i class="fa fa-upload"></i>&nbsp;Cargar Documento</span></a>
                                                <label id="L_UP"></label>
                                            </div>
                                        </div>--%>
                                        <asp:TextBox runat="server" ID="T_URL_DOCUMENTO" CssClass="form-control m-b-10" placeholder="Ingrese aquí el link (ej: www.dropbox.com/doc.pdf)"></asp:TextBox>
                                        <asp:LinkButton runat="server" ID="B_AGREGAR_URL_DOC" CssClass="btn btn-primary btn-sm btn-block" OnClick="B_AGREGAR_URL_DOC_Click"><i class="fa fa-link"></i> Agregar</asp:LinkButton>
                                    </div>
                                    <div class="col-sm-8">
                                        <h6><b><i class="fa fa-list"></i>&nbsp;LISTADO DE</b> DOCUMENTOS ADJUNTOS</h6>
                                        <asp:GridView DataKeyNames="id_compra_det" ClientIDMode="AutoID" runat="server" ID="G_DETALLE_DOCS" CssClass="table table-bordered table-condensed condensed" OnRowCommand="G_DETALLE_DOCS_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                            <HeaderStyle CssClass="thead-dark" />
                                            <Columns>
                                                <asp:BoundField DataField="id_compra_det" Visible="false" />
                                                <asp:BoundField HeaderText="FECHA" DataField="fecha_subida" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="LINK DESCARGA" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <a href="<%# Eval("url_doc") %>" target="_blank"><b><i class="fa fa-download"></i>&nbsp; <%# Eval("url_doc") %></b></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="BORRAR" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar" OnClientClick="return confirm('Desea eliminar el registro seleccionado?');">
                                                                                <i class="icon-trash"></i> BORRAR
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
                            </form>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-raised btn-primary btn-round waves-effect btn-lg" runat="server" ID="B_GUARDAR" OnClick="B_GUARDAR_Click"><i class="fa fa-save"></i>&nbsp;GUARDAR</asp:LinkButton>
                    <button type="button" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CODIGO_JAVASCRIPT" runat="server">
    <script>
        $(document).ready(function () {
            Datatables();
        });

        function ABREMODAL() {
            $('#MODAL_FACTURA').click();
        }

        function Datatables() {
            $('#<%= G_PRINCIPAL.ClientID %>').DataTable({
                "bLengthChange": false,
                destroy: true,
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

       <%-- function SubirDocumento() {
            var data = new FormData();
            var files = $("#FU_LOGO").get(0).files;
            var guid_pro = guid();
            if (files.length > 0) {
                var id_compra = document.getElementById('<%= T_ID.ClientID %>').value;
                data.append("UploadedImage", files[0]);
                var ajaxRequest = $.ajax({
                    type: "POST",
                    url: "SUBE_ARCHIVOS.aspx?tipo_doc=3&id_compra=" + id_compra + "&guid=" + guid_pro,
                    contentType: false,
                    processData: false,
                    data: data
                });
                ajaxRequest.done(function (xhr, textStatus) {
                    $('#L_UP').html("<span class='label label-success' style='font-size:12px;'><i class='fa fa-check'></i> Subido con éxito, por favor ahora guarde.</span>");
                });
            }
            else {
                alert("ERROR AL SUBIR ARCHIVO");
            }
        }--%>
</script>
</asp:Content>
