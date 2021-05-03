<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="Stock_Repuestos.aspx.cs" Inherits="AMALIA.Stock_Repuestos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BODY" runat="server">
    
    <section class="content">
        <div class="container">
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="body block-header">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:UpdatePanel runat="server" ID="UP_PRINCIPAL">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="G_PRINCIPAL" EventName="RowCommand" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div style="display: none">
                                                <a id="MODAL_GASTO" href="#modalgasto" data-toggle="modal" data-target="#modalgasto"></a>
                                                <a id="MODAL_GASTO2" href="#modalgasto2" data-toggle="modal" data-target="#modalgasto2"></a>
                                            </div>
                                            <asp:Panel runat="server" ID="PANEL_PRINCIPAL">
                                                </div>
                                                    <h2>STOCK DE REPUESTOS</h2>
                                                <hr />
                                                <div class="table-responsive">
                                                    <asp:GridView DataKeyNames="id_stock, id_producto, cantidad, nom_producto" ClientIDMode="AutoID" runat="server" ID="G_PRINCIPAL" CssClass="table table-bordered" OnRowCommand="G_PRINCIPAL_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                        <HeaderStyle CssClass="thead-dark" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="verstock">
                                                                        <i class="fa fa-list-alt"></i>
                                                                    </asp:LinkButton>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:LinkButton ToolTip="Borrar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                   <i class="fa fa-edit"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="id_stock" DataField="id_stock" Visible="false" />
                                                            <asp:BoundField HeaderText="id_producto" DataField="id_producto" Visible="false" />
                                                            <asp:BoundField HeaderText="CATEGORIA" DataField="nombre_categoria" />
                                                            <asp:BoundField HeaderText="MARCA" DataField="nombre_marca" />
                                                            <asp:BoundField HeaderText="PRODUCTO" DataField="nom_producto" />
                                                            <asp:BoundField HeaderText="CANTIDAD" DataField="cantidad" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No se encontraron resultados.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
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
    </section>
    <!-- MODAL CARGA DE GASTO GENERAL -->
    <div class="modal fade" id="modalgasto2" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel runat="server" ID="UP_GASTO_GENERAL2" UpdateMode="Conditional">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="row clearfix">
                                <div class="col-lg-12">                                    
                                    <h4 id="stock_nombre_producto" runat="server"></h4>
                                    <div class="card">
                                        <div class="body table-responsive">
                                            <asp:GridView  ClientIDMode="AutoID" runat="server" ID="G_LOG_STOCK" CssClass="table table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                <HeaderStyle CssClass="thead-dark" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="FECHA" DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField HeaderText="MOVIMIENTO" DataField="entra_sale2" />
                                                    <asp:BoundField HeaderText="USUARIO" DataField="usuario" />
                                                    <asp:BoundField HeaderText="CANTIDAD" DataField="cantidad" />
                                                    <asp:BoundField HeaderText="CANTIDAD ANTES" DataField="cantidad_inicial" />
                                                    <asp:BoundField HeaderText="CANTIDAD DESPUES" DataField="cantidad_final" />
                                                    <asp:BoundField HeaderText="DOCUMENTO COMPRA" DataField="doc_compra" />
                                                    <asp:BoundField HeaderText="PRECIO COMPRA" DataField="precio_compra" />
                                                    <asp:BoundField HeaderText="O.T." DataField="id_ot" />
                                                    <asp:BoundField HeaderText="MOTIVO" DataField="motivo" />

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
                        <div class="modal-footer">
                            <button type="button" id="CERRAR_MODAL" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="modalgasto" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="UP_GASTO_GENERAL" UpdateMode="Conditional">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <div runat="server" id="TITULO_MODAL"></div>
                        </div>
                        <div class="modal-body">

                            <hr />
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-horizontal">
                                        <div style="display: none">
                                            <asp:TextBox runat="server" ID="t_id_stock"></asp:TextBox>
                                            <asp:TextBox runat="server" ID="T_ID_PRODUCTO"></asp:TextBox>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label><b>Stock Actual</b></label>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_STOCK_ACTUAL" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group text-left">
                                                    <asp:LinkButton runat="server" ID="B_GUARDAR_STOCK" OnClick="B_GUARDAR_STOCK_Click" CssClass="btn btn-primary"><i class="fa fa-save"></i>&nbsp; Modificar</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <p class="text-purple"><b>AUMENTAR STOCK</b></p>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label>Cantidad</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_CANTIDAD" TextMode="Number" CssClass="form-control"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label>Valor Compra</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_VALOR_COMPRA" TextMode="Number" CssClass="form-control"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label>Nº Documento</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_NUM_DOCUMENTO" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label>&nbsp;</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:LinkButton runat="server" ID="B_AUMENTAR_STOCK" OnClick="B_AUMENTAR_STOCK_Click" CssClass="btn btn-sm btn-primary"><i class="fa fa-arrow-up"></i>&nbsp; AUMENTAR</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <p class="text-purple"><b>DISMINUIR STOCK</b></p>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label>Cantidad</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_CANTIDAD2" TextMode="Number" CssClass="form-control"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label>Motivo</label>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_MOTIVO" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label>&nbsp;</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:LinkButton runat="server" ID="B_DISMINUIR_STOCK" OnClick="B_DISMINUIR_STOCK_Click" CssClass="btn btn-sm btn-primary"><i class="fa fa-arrow-down"></i>&nbsp; DISMINUIR</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Cerrar</button>
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
            Datatables();
        });

        function Datatables() {
            var rows = document.getElementById('<%= G_PRINCIPAL.ClientID %>').getElementsByTagName("tr").length;
            if (rows > 1) {
                $('#<%= G_PRINCIPAL.ClientID %>').DataTable({
                    destroy: true,
                    stateSave: true,
                    dom: 'lBfrtip',
                    buttons: [
                        'copy',
                        'print'
                    ],
                    "language": {
                        "url": "assets/Spanish.json"
                    }
                });
            }
        }

        function GASTOGENERAL() {
            $('#MODAL_GASTO').click();
        }
        function GASTOGENERAL2() {
            $('#MODAL_GASTO2').click();
        }


    </script>
</asp:Content>
