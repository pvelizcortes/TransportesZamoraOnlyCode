<%@ Page Title="" Language="C#" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="Modulo_OTZ.aspx.cs" Inherits="AMALIA.Modulo_OTZ" %>

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
                                        <div class="col-sm-2">
                                            OTZ DESDE:
                                                        <br />
                                            <asp:TextBox runat="server" ID="FILTRA_OTZ_DESDE" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                            OTZ HASTA:
                                                        <br />
                                            <asp:TextBox runat="server" ID="FILTRA_OTZ_HASTA" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                            GT DESDE:
                                                        <br />
                                            <asp:TextBox runat="server" ID="FILTRA_GT_DESDE" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                            GT HASTA:
                                                        <br />
                                            <asp:TextBox runat="server" ID="FILTRA_GT_HASTA" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <br />
                                            <asp:LinkButton runat="server" ID="B_FILTRAR" ClientIDMode="AutoID" OnClick="B_FILTRAR_Click" CssClass="btn btn-primary btn-sm btn-round" OnClientClick="relojito2(true);"><i class="fa fa-search"></i> Filtrar</asp:LinkButton>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row clearfix">
                                        <div class="col-sm-12">
                                            <div class="table-responsive">
                                                <asp:GridView DataKeyNames="ID_OTZ, ID_GT" runat="server" ClientIDMode="AutoID" ID="G_OTZ" CssClass="table table-bordered tablaprincipal" OnRowCommand="G_OTZ_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                    <HeaderStyle CssClass="thead-light" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="ID_GT" DataField="ID_GT" Visible="false" />
                                                        <asp:BoundField HeaderText="ID_OTZ" DataField="ID_OTZ" Visible="false" />
                                                        <asp:BoundField HeaderText="GT" DataField="correlativo_gt" ItemStyle-Font-Bold="true" />
                                                        <%--<asp:BoundField HeaderText="OTZ" DataField="correlativo_otz" ItemStyle-Font-Bold="true" ItemStyle-CssClass="td-sticky" HeaderStyle-CssClass="td-sticky" />--%>
                                                        <asp:TemplateField HeaderText="OTZ" ItemStyle-Font-Bold="true" ItemStyle-CssClass="td-sticky" HeaderStyle-CssClass="td-sticky">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ToolTip="Ver Otz" OnClientClick="relojito(true);" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar" CssClass="text-purple">
                                                                                 <b><%# Eval("correlativo_otz") %> </b>                                                                          
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="F. INICIO" DataField="f_inicio" />
                                                        <asp:BoundField HeaderText="CONDUCTOR" DataField="nombre_completo" />
                                                        <asp:BoundField HeaderText="CAMION" DataField="patente" />
                                                        <asp:BoundField HeaderText="CLIENTE" DataField="nombre_cliente" />
                                                        <asp:BoundField HeaderText="OBRA" DataField="obra" />
                                                        <asp:BoundField HeaderText="ORIGEN" DataField="c_origen" />
                                                        <asp:BoundField HeaderText="DESTINO" DataField="c_destino" />
                                                        <asp:BoundField HeaderText="SOL-OC" DataField="d_sol_oc" />
                                                        <%-- <asp:TemplateField HeaderText="SOL-OC" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td-doc">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="t_d_sol_oc" Style="width: 200px;" Text='<%# Eval("d_sol_oc")%>'></asp:TextBox>
                                                                <span style="display: none"><%# Eval("d_sol_oc")%></span>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="sol_oc"><i class="fa fa-save" style="width:20%;padding-right:10px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="OT" DataField="d_ot" ItemStyle-CssClass="td-doc" />
                                                        <%--  <asp:TemplateField HeaderText="OT" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td-doc">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="t_d_ot" Style="width: 100px;" Text='<%# Eval("d_ot")%>'></asp:TextBox>
                                                                <span style="display: none"><%# Eval("d_ot")%></span>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="d_ot"><i class="fa fa-save" style="width:20%;padding-right:10px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="GUIA DE DESPACHO" DataField="guia" ItemStyle-CssClass="td-doc" />
                                                        <%--  <asp:TemplateField HeaderText="GUIA DE DESPACHO" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td-doc">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="t_guia" Style="width: 200px;" Text='<%# Eval("guia")%>'></asp:TextBox>
                                                                <span style="display: none"><%# Eval("guia")%></span>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="guia"><i class="fa fa-save" style="width:20%; padding-right:10px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="EEPP" DataField="d_eepp" ItemStyle-CssClass="td-doc" />
                                                        <%--  <asp:TemplateField HeaderText="EEPP" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td-doc">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="t_d_eepp" Style="width: 100px;" Text='<%# Eval("d_eepp")%>'></asp:TextBox>
                                                                <span style="display: none"><%# Eval("d_eepp")%></span>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="d_eepp"><i class="fa fa-save" style="width:20%;padding-right:10px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="GASTO" DataField="d_gasto" ItemStyle-CssClass="td-doc" />
                                                        <%--   <asp:TemplateField HeaderText="GASTO" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td-doc">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="t_d_gasto" Style="width: 100px;" Text='<%# Eval("d_gasto")%>'></asp:TextBox>
                                                                <span style="display: none"><%# Eval("d_gasto")%></span>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="d_gasto"><i class="fa fa-save" style="width:20%;padding-right:10px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="OC" DataField="d_oc" ItemStyle-CssClass="td-doc" />
                                                        <%-- <asp:TemplateField HeaderText="OC" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td-doc">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="t_d_oc" Style="width: 100px;" Text='<%# Eval("d_oc")%>'></asp:TextBox>
                                                                <span style="display: none"><%# Eval("d_oc")%></span>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="d_oc"><i class="fa fa-save" style="width:20%;padding-right:10px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="HES" DataField="d_hes" ItemStyle-CssClass="td-doc" />
                                                        <%--  <asp:TemplateField HeaderText="HES" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td-doc">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="t_d_hes" Style="width: 100px;" Text='<%# Eval("d_hes")%>'></asp:TextBox>
                                                                <span style="display: none"><%# Eval("d_hes")%></span>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="d_hes"><i class="fa fa-save" style="width:20%;padding-right:10px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="FACTURA" DataField="d_factura" ItemStyle-CssClass="td-doc text-center" ItemStyle-Font-Bold="true" />
                                                        <%--   <asp:TemplateField HeaderText="FACTURA" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td-doc">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="t_d_factura" Style="width: 100px;" Text='<%# Eval("d_factura")%>'></asp:TextBox>
                                                                <span style="display: none"><%# Eval("d_factura")%></span>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="d_factura"><i class="fa fa-save" style="width:20%;padding-right:10px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="VALOR" DataField="valor_viaje" ItemStyle-CssClass="td-val text-center" ItemStyle-Font-Bold="true" DataFormatString="{0:C0}" />
                                                        <%-- <asp:TemplateField HeaderText="VALOR" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td-val">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="t_valor_viaje" Style="width: 100px;" Text='<%# Eval("valor_viaje")%>'></asp:TextBox>
                                                                <span style="display: none"><%# Eval("valor_viaje")%></span>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="valor_viaje"><i class="fa fa-save" style="width:20%;padding-right:10px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="ESTADÍA" DataField="estadia" ItemStyle-CssClass="td-val" DataFormatString="{0:C0}" />
                                                        <%-- <asp:TemplateField HeaderText="ESTADÍA" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td-val">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="t_estadia" Style="width: 100px;" Text='<%# Eval("estadia")%>'></asp:TextBox>
                                                                <span style="display: none"><%# Eval("estadia")%></span>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="estadia"><i class="fa fa-save" style="width:20%;padding-right:10px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="ENTRADA" DataField="entradas" ItemStyle-CssClass="td-val" DataFormatString="{0:C0}" />
                                                        <%--  <asp:TemplateField HeaderText="ENTRADA" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td-val">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="t_entradas" Style="width: 100px;" Text='<%# Eval("entradas")%>'></asp:TextBox>
                                                                <span style="display: none"><%# Eval("entradas")%></span>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="entradas"><i class="fa fa-save" style="width:20%;padding-right:10px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="DOBLE CONDUCTOR" DataField="doble_conductor" ItemStyle-CssClass="td-val" DataFormatString="{0:C0}" />
                                                        <%--  <asp:TemplateField HeaderText="DOBLE CONDUCTOR" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td-val">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="t_doble_conductor" Style="width: 100px;" Text='<%# Eval("doble_conductor")%>'></asp:TextBox>
                                                                <span style="display: none"><%# Eval("doble_conductor")%></span>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="doble_conductor"><i class="fa fa-save" style="width:20%;padding-right:10px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="CARGA DESCARGA" DataField="carga_descarga" ItemStyle-CssClass="td-val" DataFormatString="{0:C0}" />
                                                        <asp:BoundField HeaderText="OTROS" DataField="otros" ItemStyle-CssClass="td-val" DataFormatString="{0:C0}" />
                                                        <%--  <asp:TemplateField HeaderText="OTROS" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td-val">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="t_otros" Style="width: 100px;" Text='<%# Eval("otros")%>'></asp:TextBox>
                                                                <span style="display: none"><%# Eval("otros")%></span>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="otros"><i class="fa fa-save" style="width:20%;padding-right:10px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="DETALLE OTROS" DataField="detalle_otros" ItemStyle-CssClass="td-val" />
                                                        <%--   <asp:TemplateField HeaderText="DETALLE OTROS" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td-val">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="t_detalle_otros" Style="width: 200px;" Text='<%# Eval("detalle_otros")%>'></asp:TextBox>
                                                                <span style="display: none"><%# Eval("detalle_otros")%></span>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="detalle_otros"><i class="fa fa-save" style="width:20%;padding-right:10px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="FLETE DE TERCERO" DataField="flete_de_tercero" ItemStyle-CssClass="td-val2" DataFormatString="{0:C0}" />
                                                        <%-- <asp:TemplateField HeaderText="FLETE DE TERCERO" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td-val2">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="t_flete_de_tercero" Style="width: 100px;" Text='<%# Eval("flete_de_tercero")%>'></asp:TextBox>
                                                                <span style="display: none"><%# Eval("flete_de_tercero")%></span>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="flete_de_tercero"><i class="fa fa-save" style="width:20%;padding-right:10px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="NOMBRE TERCERO" DataField="d_nombre_tercero" ItemStyle-CssClass="td-doc" />
                                                        <%-- <asp:TemplateField HeaderText="NOMBRE TERCERO" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td-doc">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="t_d_nombre_tercero" Style="width: 100px;" Text='<%# Eval("d_nombre_tercero")%>'></asp:TextBox>
                                                                <span style="display: none"><%# Eval("d_nombre_tercero")%></span>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="d_nombre_tercero"><i class="fa fa-save" style="width:20%;padding-right:10px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="EEPP TERCERO" DataField="d_eepp_tercero" ItemStyle-CssClass="td-doc" />
                                                        <%-- <asp:TemplateField HeaderText="EEPP TERCERO" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td-doc">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="t_d_eepp_tercero" Style="width: 100px;" Text='<%# Eval("d_eepp_tercero")%>'></asp:TextBox>
                                                                <span style="display: none"><%# Eval("d_eepp_tercero")%></span>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="d_eepp_tercero"><i class="fa fa-save" style="width:20%;padding-right:10px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="FACTURA TERCERO" DataField="d_factura_tercero" ItemStyle-CssClass="td-doc" />
                                                        <%-- <asp:TemplateField HeaderText="FACTURA TERCERO" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="td-doc">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="t_d_factura_tercero" Style="width: 100px;" Text='<%# Eval("d_factura_tercero")%>'></asp:TextBox>
                                                                <span style="display: none"><%# Eval("d_factura_tercero")%></span>
                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="d_factura_tercero"><i class="fa fa-save" style="width:20%;padding-right:10px;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="DIFERENCIA FACT." DataField="diferencia_factura" ItemStyle-CssClass="td-val" DataFormatString="{0:C0}" />
                                                        <asp:BoundField HeaderText="OBSERVACIÓN FACT. " DataField="observacion_factura" ItemStyle-CssClass="td-doc" />
                                                        <asp:BoundField HeaderText="TOTAL" DataField="suma_otz" DataFormatString="{0:C0}" ItemStyle-Font-Bold="true" HeaderStyle-CssClass="td-sticky2" ItemStyle-CssClass="td-sticky2 td-val" />
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
                            <h4 class="title text-purple" id="TITULO_MODAL_OTZ" runat="server">OTZ</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-horizontal">
                                        <div style="display: none">
                                            <asp:TextBox runat="server" ID="T_ID_OTZ"></asp:TextBox>
                                            <asp:TextBox runat="server" ID="T_ID_GT"></asp:TextBox>
                                        </div>
                                        <hr />
                                        <h4>Documentos</h4>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">SOL - OC</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_SOL_OC" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">OT</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_OT" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">GUIA DESPACHO</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_GUIA" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">EEPP</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_EEPP" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">GASTO</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_GASTO" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">OC</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_D_OC" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">HES</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_HES" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">FACTURA</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_FACTURA" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold text-purple">VALOR VIAJE ($) </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_VALOR"  TextMode="Number" CssClass="form-control border-success"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <h4>Opcionales</h4>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold col-green">Estadía (+$) </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_ESTADIA" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold col-green">Entradas (+$) </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_ENTRADAS" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold col-green">Doble Conductor (+$) </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_DOBLE_CONDUCTOR" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                          <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold col-green">Carga / Descarga (+$) </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_CARGA_DESCARGA" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold col-green">Otros (+$) </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_OTROS" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold col-green">Detalle Otros </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_OTROS_DETALLE" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold col-red">Flete de Tercero (-$) </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_FLETE_DE_TERCERO" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">Nombre de Tercero</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_NOMBRE_TERCERO" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">EEPP Tercero</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_EEPP_TERCERO" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">Factura de Tercero</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_FACTURA_DE_TERCERO" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold col-green">Diferencia Factura</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" TextMode="Number" ID="T_OTZ_DIFERENCIA_FACTURA" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">Observación Factura</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_OBSERVACION_FACTURA" CssClass="form-control"></asp:TextBox>
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
                fixedColumns: true,
                scrollY: "1500px",
                scrollX: true,
                scrollCollapse: true,
                paging: true,
                fixedColumns: {
                    leftColumns: 6
                },
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
                    targets: [2]
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
