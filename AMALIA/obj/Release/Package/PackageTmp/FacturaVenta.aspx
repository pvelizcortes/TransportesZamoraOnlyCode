<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="FacturaVenta.aspx.cs" Inherits="AMALIA.FacturaVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BODY" runat="server">
    <style>
        .cell-letra-chica {
            text-align: left !important;
            font-size: 10px !important;
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
                                                <li class="breadcrumb-item active">Facturas de Venta</li>
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
                                                                        <h5>Patente: <span runat="server" id="DIV_PATENTE"></span></h5>                                                                       
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-3 text-center">
                                                                    <h5 class="m-b-0 text-success" runat="server" id="DIV_TOTAL"></h5>
                                                                    <small>Total Ventas</small>
                                                                </div>                                                               
                                                                <div class="col-3 text-center">
                                                                    <h5 class="m-b-0" runat="server" id="DIV_NUM_DOCS"></h5>
                                                                    <small>Nº Viajes</small>
                                                                </div>
                                                                <div class="col-3 text-center">
                                                                    <h5 class="m-b-0 text-danger" runat="server" id="DIV_SIN_PAGAR"></h5>
                                                                    <small class="text-danger"><b>Facturas sin Cobrar</b></small>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>                                            
                                                <div class="row" style="border-top:1px solid lightgray;margin-top:10px;">
                                                    <div class="col-sm-12 table-responsive">
                                                        <asp:GridView DataKeyNames="num_factura, estado " ClientIDMode="AutoID" runat="server" ID="G_PRINCIPAL" OnRowDataBound="G_PRINCIPAL_RowDataBound"
                                                            CssClass="table table-bordered tablaprincipal table-hover js-exportable table-condensed condensed" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowCommand="G_PRINCIPAL_RowCommand">
                                                            <HeaderStyle CssClass="thead-dark" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText=" " ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ToolTip="Editar / Ver" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                                <i class="fa fa-eye"></i>                                                                                
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Fecha" DataField="fecha_inicio" ItemStyle-HorizontalAlign="left" DataFormatString="{0:dd/MM/yyyy}" />
                                                                <asp:BoundField HeaderText="Nº GT" DataField="correlativo_gt" ItemStyle-Font-Bold="true" />
                                                                <asp:BoundField HeaderText="Nº OTZ" DataField="correlativo_otz" ItemStyle-Font-Bold="true" />
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Factura" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ToolTip="Ver Factura" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar" CssClass="text-purple">
                                                                                 <b><%# Eval("num_factura") %> </b>                                                                          
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
                                                                <asp:BoundField HeaderText="Cliente" DataField="nombre_cliente" ItemStyle-CssClass="cell-letra-chica" />
                                                                <asp:BoundField HeaderText="Obra" DataField="nom_obra" ItemStyle-CssClass="cell-letra-chica" />
                                                                <asp:BoundField HeaderText="Total" DataField="suma_otz" ItemStyle-CssClass="text-right" DataFormatString="{0:C0}" ItemStyle-Font-Bold="true" />
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                No se encontraron resultados.
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
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
                                    <i class="fa fa-file-invoice"></i>&nbsp;<asp:Label runat="server" ID="LBL_ESTADO" Text="DETALLE DE LA FACTURA"></asp:Label></b>
                            </h4>
                            <hr />
                            <form class="form-horizontal">
                                <div style="display: none">
                                    <asp:TextBox runat="server" ID="T_ID"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="T_NUM_FACTURA"></asp:TextBox>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <small><b>FECHA EMISION *</b></small>
                                            <asp:TextBox runat="server" TextMode="Date" CssClass="form-control" ID="FECHA_EMISION"></asp:TextBox>
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
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <small><b>FECHA PAGO *</b></small>
                                            <asp:TextBox runat="server" TextMode="Date" CssClass="form-control" ID="FECHA_PAGO"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <small class="text-success"><b>TOTAL ($) *</b></small>
                                            <asp:TextBox runat="server" ID="T_TOTAL" CssClass="form-control text-right border-success" Text="0" TextMode="Number"></asp:TextBox>
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
                                        <asp:GridView DataKeyNames="id_venta_det" ClientIDMode="AutoID" runat="server" ID="G_DETALLE_DOCS" CssClass="table table-bordered table-condensed condensed" OnRowCommand="G_DETALLE_DOCS_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                            <HeaderStyle CssClass="thead-dark" />
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText=" " ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ToolTip="Ver" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Ver">
                                                                                <i class="fa fa-file-invoice"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="id_venta_det" Visible="false" />
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
                "bLengthChange": true,
                destroy: true,
                stateSave: true,
                dom: 'lBfrtip',
                buttons: [
                    'copy',
                    'print'
                ],
                "language": {
                    "url": "assets/Spanish.json",
                    "decimal": ",",
                    "thousands": "."
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
