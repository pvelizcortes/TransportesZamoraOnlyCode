<%@ Page Title="" Language="C#" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="CombustibleControl.aspx.cs" Inherits="AMALIA.CombustibleControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BODY" runat="server">
    
    <style>
        .td-sticky {
            position: -webkit-sticky;
            position: sticky;
            left: 0;
            background-color: #ccebff !important;
        }

        .td-sticky2 {
            position: -webkit-sticky;
            position: sticky;
            right: 0;
            background-color: #ccffb3;
        }

        .td-padd {
        }

        .td-doc {
            background-color: #ffffb3;
        }

        .td-val {
            background-color: #ccffb3;
        }

        .td-val2 {
            background-color: #ffb3b3;
        }
    </style>
    <section class="content">
        <div class="container">
            <asp:UpdatePanel runat="server" ID="up_1">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="G_OTZ" EventName="RowCommand" />
                    <asp:AsyncPostBackTrigger ControlID="B_FILTRAR" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="row clearfix">
                        <div class="col-sm-12">
                            <div style="display: none">
                                <a id="MODAL_OTZ" href="#modalotz" data-toggle="modal" data-target="#modalotz"></a>
                            </div>
                            <div class="card">
                                <div class="body">
                                    <div class="row clearfix">
                                        <div class="col-sm-3">
                                            <asp:LinkButton runat="server" ID="Crear_Nuevo" CssClass="btn btn-primary btn-block" OnClick="Crear_Nuevo_Click">INGRESAR NUEVO REGISTRO +</asp:LinkButton>
                                            &nbsp;
                                        </div>
                                        <div class="col-sm-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-sm-2">
                                            FECHA DESDE:
                                                        <br />
                                            <asp:TextBox runat="server" ID="FILTRO_FECHA_DESDE" CssClass="form-control" TextMode="date"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                            FECHA HASTA:
                                                        <br />
                                            <asp:TextBox runat="server" ID="FILTRO_FECHA_HASTA" CssClass="form-control" TextMode="date"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <br />
                                            <asp:LinkButton runat="server" ID="B_FILTRAR" ClientIDMode="AutoID" OnClick="B_FILTRAR_Click" CssClass="btn btn-primary btn-sm btn-round" OnClientClick="relojito2(true);"><i class="fa fa-search"></i> Filtrar</asp:LinkButton>
                                        </div>


                                    </div>
                                    <br />
                                    <hr />
                                    <div class="row clearfix">
                                        <div class="col-sm-12">
                                            <div class="table-responsive">
                                                <asp:GridView DataKeyNames="ID_LOG" runat="server" ClientIDMode="AutoID" ID="G_OTZ" CssClass="table table-bordered tablaprincipal" OnRowCommand="G_OTZ_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                    <HeaderStyle CssClass="thead-light" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                   <i class="icon-pencil"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="ID_LOG" DataField="ID_LOG" Visible="false" />
                                                        <%--<asp:BoundField HeaderText="Usuario" DataField="usuario" />--%>
                                                        <asp:BoundField HeaderText="Fecha" DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:BoundField HeaderText="Guia" DataField="guia" ItemStyle-Font-Bold="true" ItemStyle-CssClass="text-purple" />
                                                        <asp:BoundField HeaderText="Surtidor Inicio" DataField="cont_surtidor_inicial" />
                                                        <asp:BoundField HeaderText="Km. Camión" DataField="km_camion" />
                                                        <asp:BoundField HeaderText="Conductor" DataField="nombre_conductor" />
                                                        <asp:BoundField HeaderText="Patente" DataField="patente_camion" ItemStyle-Font-Bold="true" />
                                                        <asp:BoundField HeaderText="Litros" DataField="litros" ItemStyle-Font-Bold="true" ItemStyle-CssClass="text-purple" />
                                                        <asp:BoundField HeaderText="Surtidor Final" DataField="cont_surtidor_final" />
                                                        <asp:BoundField HeaderText="Stock Estanque" DataField="stock_estanque" ItemStyle-Font-Bold="true" ItemStyle-CssClass="text-success" />
                                                        <asp:BoundField HeaderText="Precio" DataField="precio" DataFormatString="{0:C0}" />
                                                        <asp:BoundField HeaderText="Total" DataField="total" DataFormatString="{0:C0}" ItemStyle-Font-Bold="true" ItemStyle-CssClass="text-danger" />
                                                        <asp:BoundField HeaderText="Factura" DataField="factura_asociada" />
                                                        <asp:BoundField HeaderText="Observación" DataField="observacion" />
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar" OnClientClick="return confirm('Desea eliminar el registro seleccionado?');">
                                                                   <i class="icon-trash"></i>
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
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </section>
    <!-- Modales -->
    <div class="modal fade" id="modalotz" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="UP_OTZ" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="B_GUARDAR_OTZ" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="title text-purple" id="TITULO_MODAL_OTZ" runat="server">Control de Combustibles</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <p><small>Todos los campos son obligatorios *</small></p>
                                    <div class="form-horizontal">
                                        <div style="display: none">
                                            <asp:TextBox runat="server" ID="T_ID_LOG"></asp:TextBox>
                                        </div>
                                        <hr />
                                        <div id="div_read_only" runat="server">
                                            <div class="row clearfix">
                                                <div class="col-sm-4 form-control-label">
                                                    <label class="font-bold">Fecha</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <asp:TextBox runat="server" TextMode="Date" ID="modal_fecha" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row clearfix">
                                                <div class="col-sm-4 form-control-label">
                                                    <label class="font-bold">Nº guía</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <asp:TextBox runat="server" ID="modal_num_guia" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row clearfix">
                                                <div class="col-sm-4 form-control-label">
                                                    <label class="font-bold">Surtidor (inicio)</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <asp:TextBox runat="server" ID="modal_surtidor_inicio" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row clearfix">
                                                <div class="col-sm-4 form-control-label">
                                                    <label class="font-bold">Km camión</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <asp:TextBox runat="server" ID="modal_km_camion" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row clearfix">
                                                <div class="col-sm-4 form-control-label">
                                                    <label class="font-bold">Nombre conductor</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <asp:TextBox runat="server" ID="modal_nombre_conductor" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row clearfix">
                                                <div class="col-sm-4 form-control-label">
                                                    <label class="font-bold">Patente camión</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <asp:TextBox runat="server" ID="modal_patente_camion" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row clearfix">
                                                <div class="col-sm-4 form-control-label">
                                                    <label class="font-bold">Litros</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <asp:TextBox runat="server" ID="modallitros" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row clearfix">
                                                <div class="col-sm-4 form-control-label">
                                                    <label class="font-bold">Surtidor (final)</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <asp:TextBox runat="server" ID="modal_surtidor_final" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row clearfix">
                                                <div class="col-sm-4 form-control-label">
                                                    <label class="font-bold">Stock estanque</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <asp:TextBox runat="server" ID="modal_stock_estanque" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row clearfix">
                                                <div class="col-sm-4 form-control-label">
                                                    <label class="font-bold">Precio</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <asp:TextBox runat="server" ID="modal_precio" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row clearfix">
                                                <div class="col-sm-4 form-control-label">
                                                    <label class="font-bold">Total</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <asp:TextBox runat="server" ID="modal_total" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">Factura Asociada <small class="text-warning">(Opcional)</small></label>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="modal_factura_asociada" CssClass="form-control border-warning"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">Observacion <small class="text-warning">(Opcional)</small></label>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="modal_observacion" CssClass="form-control border-warning"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton runat="server" ID="B_GUARDAR_OTZ" ClientIDMode="AutoID" OnClick="B_GUARDAR_OTZ_Click" CssClass="btn btn-primary btn-round waves-effect"><i class="fa fa-save"></i> GUARDAR</asp:LinkButton>
                            <button type="button" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal" id="CERRAR_MODAL_OTZ">Cerrar</button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CODIGO_JAVASCRIPT" runat="server">
    <script>
        $(document).ready(function () {
           <%-- relojito2(true);
            setTimeout(function () { document.getElementById('<%= B_FILTRAR.ClientID %>').click(); }, 1000);--%>
            DT_OTZ();
        });

        function DT_OTZ() {
            $('#<%= G_OTZ.ClientID %>').DataTable({
                paging: true,
                destroy: true,
                stateSave: true,
                dom: 'Bfrtip',
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
        function OTZ() {
            $('#MODAL_OTZ').click();
            relojito(false);
        }
        function CerrarModalOTZ() {
            $('#CERRAR_MODAL_OTZ').click();
        }
    </script>
</asp:Content>
