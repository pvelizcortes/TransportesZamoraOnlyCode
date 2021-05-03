<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="COMPRA.aspx.cs" Inherits="AMALIA.COMPRA" %>

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
                                            <asp:AsyncPostBackTrigger ControlID="B_LIMPIAR_CAMPOS" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div style="display: none">
                                                <a id="MODAL_GASTO" href="#modalgasto" data-toggle="modal" data-target="#modalgasto"></a>
                                            </div>
                                            <asp:Panel runat="server" ID="PANEL_PRINCIPAL">
                                                <h2>
                                                    <table>
                                                        <tr>
                                                            <td>COMPRAS DEL DÍA:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="FITRO_FECHA" CssClass="form-control" Style="width: 100%;" TextMode="Date"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton runat="server" ID="B_FILTRAR" CssClass="btn btn-round btn-primary btn-sm" ClientIDMode="AutoID" OnClick="B_FILTRAR_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </h2>
                                                <hr />
                                                <h2>
                                                    <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-raised btn-success waves-effect" runat="server" ID="B_LIMPIAR_CAMPOS" OnClick="B_LIMPIAR_CAMPOS_Click">+ INGRESAR UNA NUEVA COMPRA</asp:LinkButton></h2>
                                                <hr />
                                                <div class="table-responsive">
                                                    <asp:GridView DataKeyNames="ID_COMPRA,cantidad, id_producto, id_cat_producto, tipo_embalaje" ClientIDMode="AutoID" runat="server" ID="G_PRINCIPAL" CssClass="table table-bordered" OnRowCommand="G_PRINCIPAL_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                        <HeaderStyle CssClass="thead-dark" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="ID" DataField="id_producto" Visible="false" />
                                                            <asp:BoundField HeaderText="ID" DataField="id_cat_producto" Visible="false" />
                                                            <asp:BoundField HeaderText="ID" DataField="tipo_embalaje" Visible="false" />
                                                            <asp:BoundField HeaderText="ID" DataField="ID_COMPRA" />
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="EDITAR" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                     <i class="icon-pencil"></i> EDITAR
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="FECHA" DataField="fecha_compra" DataFormatString="{0:dd/MM/yyyy}" />
                                                            <asp:BoundField HeaderText="PROVEEDOR" DataField="nombre" />
                                                            <asp:BoundField HeaderText="PRODUCTO" DataField="nombre_producto" />
                                                            <asp:BoundField HeaderText="CATEGORIA" DataField="nombre_categoria" />
                                                            <asp:BoundField HeaderText="EMBALAJE" DataField="tipo_embalaje" />
                                                            <asp:BoundField HeaderText="CANTIDAD" DataField="cantidad" DataFormatString="{0:N0}" />
                                                            <asp:BoundField HeaderText="PRECIO" DataField="precio" DataFormatString="{0:C0}" />
                                                            <asp:BoundField HeaderText="TOTAL" DataField="total_cantprod" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" DataFormatString="{0:C0}" />
                                                            <asp:BoundField HeaderText="COMISION" DataField="comision" DataFormatString="{0:C0}" />
                                                            <asp:BoundField HeaderText="FLETE" DataField="flete" DataFormatString="{0:C0}" />
                                                            <asp:BoundField HeaderText="CARGA" DataField="carga" DataFormatString="{0:C0}" />
                                                            <asp:BoundField HeaderText="OTROS" DataField="otros" DataFormatString="{0:C0}" />
                                                             <asp:BoundField HeaderText="TOTAL GENERAL" DataField="total" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" DataFormatString="{0:C0}" />
                                                            <asp:BoundField HeaderText="FORMA DE PAGO" DataField="forma_pago" />
                                                            <asp:BoundField HeaderText="DETALLE PAGO" DataField="detalle_pago" />
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
    <div class="modal fade" id="modalgasto" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="UP_GASTO_GENERAL">
                        <ContentTemplate>
                            <h4>
                                <b>
                                    <asp:Label runat="server" ID="LBL_ESTADO" Text="INGRESANDO NUEVA COMPRA"></asp:Label></b>
                            </h4>
                            <hr />
                            <form class="form-horizontal">
                                <div style="display: none">
                                    <asp:TextBox runat="server" ID="T_ID"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="t_aux_prod"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="t_aux_cat"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="t_aux_embalaje"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="t_aux_cantidad"></asp:TextBox>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-sm-4 form-control-label">
                                        <b>PROVEEDOR</b> <small>(OPCIONAL)</small>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <asp:DropDownList runat="server" ID="CB_PROVEEDOR" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-sm-4 form-control-label">
                                        <b>FECHA</b>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <asp:TextBox runat="server" TextMode="Date" CssClass="form-control" ID="FECHA_COMPRA"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <h6 class="text-purple"><b>PRODUCTO Y EMBALAJE</b></h6>
                                <div class="row clearfix">
                                    <div class="col-sm-4 form-control-label">
                                        <b>PRODUCTO</b>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <asp:DropDownList runat="server" ID="CB_PRODUCTO" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="CB_PRODUCTO_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-sm-4 form-control-label">
                                        <b>CATEGORIA</b>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <asp:DropDownList runat="server" ID="CB_CATEGORIA" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-sm-4 form-control-label">
                                        <b>EMBALAJE</b>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <asp:DropDownList runat="server" ID="CB_EMBALAJE" CssClass="form-control">
                                                <asp:ListItem Text="KILOS" Value="KILOS" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="MALLAS" Value="MALLAS"></asp:ListItem>
                                                <asp:ListItem Text="CAJAS" Value="CAJAS"></asp:ListItem>
                                                <asp:ListItem Text="BULTOS" Value="BULTOS"></asp:ListItem>
                                                <asp:ListItem Text="BINS" Value="BINS"></asp:ListItem>
                                                <asp:ListItem Text="OTRO" Value="OTRO"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <h6 class="text-purple"><b>CANTIDAD Y VALOR</b></h6>
                                <div class="row clearfix">
                                    <div class="col-sm-4 form-control-label">
                                        <b>CANTIDAD</b>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="T_CANTIDAD" oninput="sumar();" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-sm-4 form-control-label">
                                        <b>PRECIO</b>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="T_PRECIO" oninput="sumar();" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-sm-4 form-control-label">
                                        <b>TOTAL</b>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="T_TOTAL" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-sm-4 form-control-label">
                                        <b>COMISION</b>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="T_COMISION" oninput="sumar();" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-sm-4 form-control-label">
                                        <b>FLETE</b>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="T_FLETE" oninput="sumar();" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-sm-4 form-control-label">
                                        <b>CARGA</b>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="T_CARGA" oninput="sumar();" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-sm-4 form-control-label">
                                        <b>OTROS</b>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="T_OTROS" oninput="sumar();" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <h6 class="text-purple"><b>INFORMACIÓN DEL PAGO</b></h6>
                                <div class="row clearfix">
                                    <div class="col-sm-4 form-control-label">
                                        <b>MODO DE PAGO</b>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <asp:DropDownList runat="server" ID="CB_FORMA_PAGO" CssClass="form-control">
                                                <asp:ListItem Text="EFECTIVO" Value="EFECTIVO" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="CHEQUE" Value="CHEQUE"></asp:ListItem>
                                                <asp:ListItem Text="CREDITO" Value="CREDITO"></asp:ListItem>
                                                <asp:ListItem Text="TRANSFERENCIA" Value="TRANSFERENCIA"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-sm-4 form-control-label">
                                        <b>DETALLE DE PAGO</b>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="T_DETALLE_PAGO" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                </div>
                            </form>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-raised btn-success btn-round waves-effect btn-lg" runat="server" ID="B_GUARDAR" OnClick="B_GUARDAR_Click"><i class="fa fa-save"></i>&nbsp;GUARDAR COMPRA</asp:LinkButton>
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

        function GASTOGENERAL() {
            $('#MODAL_GASTO').click();
        }

        function Datatables() {
            var rows = document.getElementById('<%= G_PRINCIPAL.ClientID %>').getElementsByTagName("tr").length;
            if (rows > 2) {
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

        function sumar() {
            try {
                var com = 0;
                var flete = 0;
                var carga = 0;
                var otros = 0;

                var cantidad = parseInt($('#<%= T_CANTIDAD.ClientID %>').val());
                var precio = parseInt($('#<%= T_PRECIO.ClientID %>').val());
                var total = cantidad * precio;

                if ($('#<%= T_COMISION.ClientID %>').val() != '') {
                    com = parseInt($('#<%= T_COMISION.ClientID %>').val());
                }
                if ($('#<%= T_FLETE.ClientID %>').val() != '') {
                    flete = parseInt($('#<%= T_FLETE.ClientID %>').val());
                }
                if ($('#<%= T_CARGA.ClientID %>').val() != '') {
                    carga = parseInt($('#<%= T_CARGA.ClientID %>').val());
                }
                if ($('#<%= T_OTROS.ClientID %>').val() != '') {
                    otros = parseInt($('#<%= T_OTROS.ClientID %>').val());
                }

                total = (total + com + flete + carga + otros);
                $('#<%= T_TOTAL.ClientID %>').val(total);
            }
            catch (ex) {

            }

        }
    </script>
</asp:Content>
