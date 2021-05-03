<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/AmaliaGD.Master" AutoEventWireup="true" CodeBehind="GD_CAMION.aspx.cs" Inherits="AMALIA.GD_CAMION" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BODY" runat="server">
    
    <style>
     
    </style>
    <section class="content">
        <div class="container">
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:UpdatePanel runat="server" ID="UP_PRINCIPAL" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="G_PRINCIPAL" EventName="RowCommand" />

                                            <asp:AsyncPostBackTrigger ControlID="B_VOLVER" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="B_GUARDAR" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="B_AGREGAR_MANTENCION" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="B_LISTAR_MANTENCIONES" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Panel runat="server" ID="PANEL_PRINCIPAL">
                                                <div class="body table-responsive">
                                                    <h5>Listado <span class="text-purple"><b>Camiones</b>&nbsp;<i class="fa fa-truck"></i></span></h5>
                                                    <asp:GridView DataKeyNames="ID_CAMION, status, dias_ag, atrasadas" ClientIDMode="AutoID" runat="server" ID="G_PRINCIPAL" CssClass="table table-bordered table-striped table-hover table-sm table-condensed" OnRowCommand="G_PRINCIPAL_RowCommand" OnRowDataBound="G_PRINCIPAL_RowDataBound" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                        <HeaderStyle CssClass="thead-dark" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="ID_CAMION" DataField="ID_CAMION" Visible="false" />
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Patente Tracto" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CssClass="text-black" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                        <b><%# Eval("patente") %></b> 
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <div id="div_estado" runat="server"></div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Kilometraje" DataField="kilometraje" />
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Días Prox. Mantención" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <div id="div_estado1" runat="server"></div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Mantenciones Atrasadas" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <div id="div_estado2" runat="server"></div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <%--  <asp:BoundField HeaderText="Días Prox. Mantención" DataField="dias_prox_mantencion" />
                                                            <asp:BoundField HeaderText="Mantenciones Atrasadas" DataField="mantenciones_atrasadas" />--%>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No se encontraron resultados.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="PANEL_DETALLE1" Visible="false">
                                                <div style="display: none">
                                                    <asp:TextBox runat="server" ID="T_ID_CAMION"></asp:TextBox>
                                                    <asp:TextBox runat="server" ID="T_ID_GDCAMION"></asp:TextBox>
                                                    <a id="MODAL_GASTO" href="#modalgasto" data-toggle="modal" data-target="#modalgasto"></a>
                                                    <a id="MODAL_GASTO2" href="#modalmantencion" data-toggle="modal" data-target="#modalmantencion"></a>
                                                    <a id="MODAL_GASTO3" href="#modalmantencion3" data-toggle="modal" data-target="#modalmantencion3"></a>
                                                    <a id="MODAL_GASTO4" href="#modalmantencion4" data-toggle="modal" data-target="#modalmantencion4"></a>
                                                    <a id="MODAL_LISTA_MANTECIONES" href="#modalmantencion2" data-toggle="modal" data-target="#modalmantencion2"></a>
                                                    <asp:LinkButton runat="server" ID="B_ACTUALIZAR_DOCS" OnClick="B_ACTUALIZAR_DOCS_Click" ClientIDMode="Static"></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="B_ACTUALIZAR_MANT" OnClick="B_ACTUALIZAR_MANT_Click" ClientIDMode="Static"></asp:LinkButton>
                                                </div>
                                                <div class="body">
                                                    <div class="row">
                                                        <div class="col-sm-4" style="border-right: 1px solid lightgray;">
                                                            <h6>Información del Camión&nbsp;<i class="fa fa-truck"></i></h6>
                                                            <hr />
                                                            <div class="form-horizontal">
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label class="font-bold">Patente</label>
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_PATENTE" CssClass="tz-input font-bold text-purple" ReadOnly="true"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix ">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label class="font-bold">Marca</label>
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_MARCA" CssClass="tz-input" ReadOnly="true"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix ">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label class="font-bold">Modelo</label>
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_MODELO" CssClass="tz-input" ReadOnly="true"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix ">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label class="font-bold">Año</label>
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_ANO" CssClass="tz-input" ReadOnly="true"></asp:TextBox>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix ">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label class="font-bold">Nº Motor</label>
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="t_motor" CssClass="tz-input" ReadOnly="true"></asp:TextBox>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix ">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label class="font-bold">Nº Chasis</label>
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="t_chasis" CssClass="tz-input" ReadOnly="true"></asp:TextBox>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix ">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label class="font-bold">Nº VIN</label>
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="t_vin" CssClass="tz-input" ReadOnly="true"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <hr />
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label class="font-bold">Kilometraje *</label>
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" TextMode="number" ID="T_KILOMETRAJE" Text="0" CssClass="form-control form-control-success text-right"></asp:TextBox></span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix ">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label class="font-bold">Status</label>
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:RadioButton runat="server" GroupName="activo" ID="RB_ACTIVO" Text="Operativo" Checked="true" />
                                                                            <asp:RadioButton runat="server" GroupName="activo" ID="RB_ACTIVO2" Text="Fuera de Servicio" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4" style="border-right: 1px solid lightgray;">
                                                            <h6>Vencimiento de Documentos&nbsp;<i class="fa fa-file"></i></h6>
                                                            <hr />
                                                            <div>
                                                                <div class="row clearfix ">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label class="font-bold">Venc. Rev. Técnica*</label>

                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group text-right">
                                                                            <asp:TextBox runat="server" TextMode="Date" ID="T_F_REV_TECNICA" CssClass="form-control"></asp:TextBox></span>
                                                                            <asp:Label runat="server" ID="LBL_F_REVTECNICA" Text="120 días restantes"></asp:Label>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix ">
                                                                     <div class="col-sm-4 form-control-label">
                                                                        <small>Documento Cargado:</small>
                                                                    </div>
                                                                    <div class="col-sm-8">                                                                       
                                                                            <div class="input-group">
                                                                                <asp:TextBox runat="server" ID="T_REV_NOMBRE_REAL" Text="No hay archivo cargado" CssClass="form-control"></asp:TextBox>
                                                                                <span class="input-group-addon" onclick="Verrevtecnica();" style="cursor:pointer;" title="VER ARCHIVO">
                                                                                    <i class="fa fa-eye text-primary"></i>
                                                                                </span>
                                                                            </div>
                                                                           
                                                                           <%-- <label style="font-size: 11px;">Documento Cargado</label>--%>
                                                                            <%--<a onclick="Verrevtecnica();" style="cursor: pointer;"><span class="badge badge-primary"><i class="fa fa-eye"></i>&nbsp;Ver Documento</span></a>--%>                                                                      
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        &nbsp;
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <input id="FU_LOGO3" style="font-size: 10px; margin-bottom: 5px;" type="file" title="Cargar Revisión Técnica." />
                                                                        <a onclick="SubirDocumento3();"><span class="btn btn-primary btn-block btn-sm"><i class="fa fa-upload"></i>&nbsp;Cargar Documento</span></a>
                                                                        <label id="L_UP3"></label>
                                                                        <div style="display: none">
                                                                            <asp:TextBox runat="server" ID="T_REV_NOMBRE_BD"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                              <hr />
                                                                <div class="row clearfix ">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label class="font-bold">Venc. Permiso Circulación*</label>
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group text-right">
                                                                            <asp:TextBox runat="server" TextMode="Date" ID="T_F_PERMISO" CssClass="form-control"></asp:TextBox></span>
                                                                             <asp:Label runat="server" ID="LBL_F_PERMISO" Text="120 días restantes"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                              
                                                                <div class="row clearfix ">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label class="font-bold">Venc. Seguro Obligatorio*</label>
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group text-right">
                                                                            <asp:TextBox runat="server" TextMode="Date" ID="T_F_SEGURO" CssClass="form-control"></asp:TextBox></span>
                                                                             <asp:Label runat="server" ID="LBL_F_SEGURO" Text="120 días restantes"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                               <hr />
                                                                <div class="row clearfix ">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label class="font-bold">Faena</label>
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList ID="CB_FAENA" runat="server" CssClass="form-control combopro">
                                                                                <asp:ListItem Text="Sin Faena asociada" Value="Sin Faena asociada" Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Text="SQM" Value="SQM"></asp:ListItem>
                                                                                <asp:ListItem Text="Antucoya" Value="Antucoya"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix ">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label class="font-bold">Venc. Cert. de Faena</label>
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group text-right">
                                                                            <asp:TextBox runat="server" TextMode="Date" ID="T_VENC_FAENA" CssClass="form-control"></asp:TextBox></span>
                                                                             <asp:Label runat="server" ID="LBL_VENC_FAENA" Text="120 días restantes"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <h6>Mantenciones&nbsp;<i class="fa fa-wrench"></i></h6>
                                                            <hr />
                                                            <div class="form-horizontal">
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label class="font-bold">Km última mantención</label>
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_ULTIMA_MANTENCION" CssClass="tz-input" ReadOnly="true"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix ">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label class="font-bold">Fecha última mantención</label>
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_FECHA_ULTIMA_MANTENCION" CssClass="tz-input" ReadOnly="true"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix ">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label class="font-bold">Días. prox. mantención</label>
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_PROXIMA_MANTENCION" CssClass="tz-input" ReadOnly="true"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix ">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label class="font-bold">Mantenciones atrasadas</label>
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_MANTENCIONES_ATRASADAS" CssClass="tz-input" ReadOnly="true"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <hr />
                                                                <div class="row clearfix ">
                                                                    <div class="col-sm-12 text-center">
                                                                        <div class="form-group text-left">
                                                                            <span>
                                                                                <asp:LinkButton runat="server" ID="B_AGREGAR_MANTENCION" ClientIDMode="AutoID" CssClass="text-purple" OnClick="B_AGREGAR_MANTENCION_Click" ToolTip="NUEVA MANTENCION"><i class="fa fa-plus"></i>&nbsp;Ingresar Mantencion</asp:LinkButton><br />
                                                                                <asp:LinkButton runat="server" ID="B_LISTAR_MANTENCIONES" ClientIDMode="AutoID" CssClass="text-purple" OnClick="B_LISTAR_MANTENCIONES_Click" ToolTip="VER MANTENCIONES"><i class="fa fa-list"></i>&nbsp;Ver Mantenciones</asp:LinkButton><hr />
                                                                                <asp:LinkButton runat="server" ID="B_AGENDAR_MANTENCION" ClientIDMode="AutoID" CssClass="text-purple" OnClick="B_AGENDAR_MANTENCION_Click" ToolTip="AGENDAR"><i class="fa fa-calendar-check"></i>&nbsp;Agendar Mantención</asp:LinkButton><br />
                                                                                <asp:LinkButton runat="server" ID="B_LISTAR_AGENDADAS" ClientIDMode="AutoID" CssClass="text-purple" OnClick="B_LISTAR_AGENDADAS_Click" ToolTip="VER AGENDADOS"><i class="fa fa-list"></i>&nbsp;Ver Agendados</asp:LinkButton><br />
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-12 text-center">
                                                            <div class="row clearfix ">
                                                                <div class="col-sm-12">
                                                                    <div class="form-group text-center">
                                                                        <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-primary btn-sm" runat="server" ID="B_GUARDAR" OnClick="B_GUARDAR_Click" ToolTip="ACTUALIZAR INFORMACION"><i class="fa fa-save"></i>&nbsp; GUARDAR</asp:LinkButton>
                                                                        <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-primary btn-sm" runat="server" ID="B_VOLVER" OnClick="B_VOLVER_Click" ToolTip="VOLVER AL LISTADO DE CAMIONES"><i class="fa fa-undo"></i>&nbsp;VOLVER</asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
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
    <!-- .MODAL MANT. || INGRESAR MANTENCION -->
    <div class="modal fade" id="modalmantencion" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel runat="server" ID="UP_MANTENCION" UpdateMode="Conditional">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-body">
                            <h6><b>Ingresando nueva mantención <i class="fa fa-wrench text-purple"></i></b></h6>
                            <hr />
                            <div class="row clearfix">
                                <div class="col-lg-12">
                                    <div style="display: none">
                                        <asp:TextBox runat="server" ID="T_ID_MANTENCION"></asp:TextBox>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-sm-4 form-control-label">
                                            <label class="font-bold">Mantencion Asociada*:</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:DropDownList runat="server" ID="CB_MANT_ASOCIADA" CssClass="form-control text-left">
                                                    <asp:ListItem Text="Tracto" Value="Tracto"></asp:ListItem>
                                                    <asp:ListItem Text="Abastecimiento" Value="Abastecimiento"></asp:ListItem>
                                                    <asp:ListItem Text="Otro" Value="Otro"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row clearfix ">
                                        <div class="col-sm-4 form-control-label">
                                            <label class="font-bold">Tipo Mantencion*:</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:DropDownList runat="server" ID="CB_TIPO_MANTENCION" CssClass="form-control text-left">
                                                    <asp:ListItem Text="Preventiva" Value="Preventiva"></asp:ListItem>
                                                    <asp:ListItem Text="Correctiva" Value="Correctiva"></asp:ListItem>
                                                    <asp:ListItem Text="Ambas" Value="Ambas"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-sm-4 form-control-label">
                                            <label class="font-bold">Kilometraje mantención *</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:TextBox runat="server" TextMode="number" ID="T_KILOMETRAJE_MANTENCION" CssClass="form-control text-left"></asp:TextBox></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row clearfix ">
                                        <div class="col-sm-4 form-control-label">
                                            <label class="font-bold">Mantención Tipo *</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:DropDownList runat="server" ID="CB_TIPO_MANTENCION_2" CssClass="form-control text-left">
                                                    <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                    <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                    <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                    <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-sm-4 form-control-label">
                                            <label class="font-bold">Fecha de mantención *</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:TextBox runat="server" TextMode="Date" ID="T_FECHA_MANTENCION" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-sm-4 form-control-label">
                                            <label class="font-bold">Adjuntar Respaldo Mantención</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <small>Debe seleccionar el archivo y luego cargarlo*</small><br />
                                            <input id="FU_LOGO2" type="file" class="input-sm form-control" />
                                            <a class="btn btn-primary btn-block" style="color: white;" onclick="SubirDocumento2()"><i class="fa fa-upload"></i>&nbsp;CARGAR ARCHIVO</a>
                                            <label id="L_UP2"></label>
                                            <div style="display: none">
                                                <asp:TextBox runat="server" ID="T_NOMBRE_ARCHIVO_BD"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="T_NOMBRE_ARCHIVO_REAL"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-sm-4 form-control-label">
                                            <label class="font-bold">Observación</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="T_OBSERVACION_MANTENCION" Rows="4" CssClass="form-control" Style="border: 1px solid lightgray;"></asp:TextBox></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton runat="server" ID="B_GUARDAR_MANTENCION" CssClass="btn btn-primary btn-round" OnClick="B_GUARDAR_MANTENCION_Click"><i class="fa fa-save"></i>&nbsp; GUARDAR</asp:LinkButton><br />
                            <button type="button" id="CERRAR_MODAL2" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <!-- .MODAL MANT. || LISTAR MANTENCION -->
    <div class="modal fade" id="modalmantencion2" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel runat="server" ID="UP_MANTENCION_LISTADO" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="G_MANTENCIONES" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <div id="LBL_MANT_PATENTE" runat="server"></div>
                        </div>
                        <div class="modal-body">
                            <div class="row clearfix">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <asp:GridView Font-Size="12px" DataKeyNames="ID_DETALLE_MANT, id_gd_camion, nombre_archivo_bd" Width="100%" ClientIDMode="AutoID" runat="server" ID="G_MANTENCIONES" CssClass="table table-bordered table-striped table-sm condensed" OnRowCommand="G_MANTENCIONES_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                            <HeaderStyle CssClass="thead-dark" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Id." DataField="ID_DETALLE_MANT" />
                                                <asp:BoundField HeaderText="Fecha Mant." DataField="fecha_mantencion" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="id_gd_camion" DataField="id_gd_camion" Visible="false" />
                                                <asp:BoundField HeaderText="nombre_archivo_bd" DataField="nombre_archivo_bd" Visible="false" />
                                                <asp:BoundField HeaderText="Km. Mant." DataField="kilometraje_mantencion" DataFormatString="{0:#,##0}" />
                                                <asp:BoundField HeaderText="Mant. Asociada" DataField="mantecion_asociada" />
                                                <asp:BoundField HeaderText="Mant. Tipo" DataField="mantencion_tipo" />
                                                <asp:BoundField HeaderText="Tipo Mant." DataField="tipo_mantecion" />
                                                <asp:BoundField HeaderText="Observación" DataField="observacion" HeaderStyle-Width="100%" ItemStyle-CssClass="wrapsito" />
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Opción" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Verarchivo" ToolTip="Ver Documento">
                                                                                <i class="fa fa-file fa-2x"></i>  
                                                        </asp:LinkButton>
                                                        &nbsp;&nbsp;
                                                                         <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar" ToolTip="Borrar" OnClientClick="return confirm('Desea eliminar el registro seleccionado?');">
                                                                                <i class="fa fa-trash fa-2x"></i>                                                                                
                                                                                                                 
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
                        <div class="modal-footer">
                            <button type="button" id="CERRAR_MODAL22" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <!-- .MODAL MANT. || AGENDAR MANTENCION -->
    <div class="modal fade" id="modalmantencion3" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel runat="server" ID="UP_AGENDAR" UpdateMode="Conditional">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-body">
                            <h6><b>Agendando nueva mantención <i class="fa fa-calendar-check text-purple"></i></b></h6>
                            <hr />
                            <div class="row clearfix">
                                <div class="col-lg-12">
                                    <div style="display: none">
                                        <asp:TextBox runat="server" ID="T_ID_AGENDAR"></asp:TextBox>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-sm-4 form-control-label">
                                            <label class="font-bold">Nombre mantención *</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:TextBox runat="server" ID="T_AG_NOMBRE" CssClass="form-control text-left"></asp:TextBox></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-sm-4 form-control-label">
                                            <label class="font-bold">Fecha mantención *</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:TextBox runat="server" TextMode="Date" ID="T_AG_FECHA" CssClass="form-control text-left"></asp:TextBox></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-sm-4 form-control-label">
                                            <label class="font-bold">Prioridad*:</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:DropDownList runat="server" ID="CB_PRIORIDAD_MANT" CssClass="form-control text-left">
                                                    <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                                                    <asp:ListItem Text="Baja" Value="Baja"></asp:ListItem>
                                                    <asp:ListItem Text="Alta" Value="Alta"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-sm-4 form-control-label">
                                            <label class="font-bold">Dias de Anterioridad *</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:TextBox runat="server" TextMode="number" ID="T_AG_DIAS_ANTERIORIDAD" CssClass="form-control text-left"></asp:TextBox></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-sm-4 form-control-label">
                                            <label class="font-bold">Observación</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="T_AG_OBSERVACION" Rows="4" CssClass="form-control" Style="border: 1px solid lightgray;"></asp:TextBox></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton runat="server" ID="B_GUARDAR_AG" CssClass="btn btn-primary btn-round" OnClick="B_GUARDAR_AG_Click"><i class="fa fa-save"></i>&nbsp; GUARDAR</asp:LinkButton><br />
                            <button type="button" id="CERRAR_MODAL3" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <!-- .MODAL MANT. || LISTAR AGENDADOS -->
    <div class="modal fade" id="modalmantencion4" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel runat="server" ID="UP_LISTA_AGENDADOS" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="G_AG_MANTENCIONES" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <div id="LBL_MANT_PATENTE4" runat="server"></div>
                        </div>
                        <div class="modal-body">
                            <div class="row clearfix">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <asp:GridView Font-Size="12px" DataKeyNames="id_mant_pendiente" Width="100%" ClientIDMode="AutoID" runat="server" ID="G_AG_MANTENCIONES" CssClass="table table-bordered table-striped table-sm condensed" OnRowCommand="G_AG_MANTENCIONES_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                            <HeaderStyle CssClass="thead-dark" />
                                            <Columns>
                                                <asp:BoundField HeaderText="id_mant_pendiente" DataField="id_mant_pendiente" Visible="false" />
                                                <asp:BoundField HeaderText="id_gd_camion" DataField="id_gd_camion" Visible="false" />
                                                <asp:BoundField HeaderText="Mantención" DataField="nombre_mant_pendiente" />
                                                <asp:BoundField HeaderText="Fecha Mant." DataField="fecha_mant" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="Prioridad" DataField="prioridad" />
                                                <asp:BoundField HeaderText="Dias anticipacion" DataField="aviso_dias_anticipacion" />
                                                <asp:BoundField HeaderText="Observación" DataField="observacion" HeaderStyle-Width="100%" ItemStyle-CssClass="wrapsito" />
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Opción" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar" ToolTip="Borrar" OnClientClick="return confirm('Desea eliminar el registro seleccionado?');">
                                                                                <i class="fa fa-trash fa-2x"></i>                                                                                
                                                                                                                 
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
                        <div class="modal-footer">
                            <button type="button" id="CERRAR_MODAL4" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CODIGO_JAVASCRIPT" runat="server">
    <script>
        $(document).ready(function () {
            Datatables();
        });
        function Datatables() {
            try {
                $('#<%= G_PRINCIPAL.ClientID %>').DataTable({
                    "lengthMenu": [[50, 25, 100, -1], [50, 25, 100, "All"]],
                    "pageLength": 50,
                    destroy: true,
                    dom: 'lBfrtip',
                    buttons: [
                        'copy', 'print'
                    ],
                    "language": {
                        "url": "assets/Spanish.json"
                    }
                });
            }
            catch (ex) {

            }
        }

        function VerArchivo(archivo, id_camion) {
            var win = window.open('Documentos/Camiones/DOCS/' + id_camion + '/' + archivo, '_blank');
            win.focus();
        }

        function GASTOGENERAL2() {
            $('#MODAL_GASTO2').click();
        }
        function GASTOGENERAL3() {
            $('#MODAL_GASTO3').click();
        }
        function GASTOGENERAL4() {
            $('#MODAL_GASTO4').click();
        }

        function ABREMODALMANTECIONES() {
            $('#MODAL_LISTA_MANTECIONES').click();
        }

        function SubirDocumento2() {
            var data = new FormData();
            var files = $("#FU_LOGO2").get(0).files;
            var guid_pro = guid();
            if (files.length > 0) {
                var id_camion = document.getElementById('<%= T_ID_CAMION.ClientID %>').value;
                data.append("UploadedImage", files[0]);
                var ajaxRequest = $.ajax({
                    type: "POST",
                    url: "SUBE_ARCHIVOS.aspx?tipo_doc=2&id_camion=" + id_camion + "&guid=" + guid_pro,
                    contentType: false,
                    processData: false,
                    data: data
                });
                ajaxRequest.done(function (xhr, textStatus) {
                    $('#L_UP2').html("<span class='text-success' style='font-size:12px;'><i class='fa fa-check'></i> Subido con éxito, por favor ahora guarde.</span>");
                    document.getElementById('<%= T_NOMBRE_ARCHIVO_BD.ClientID %>').value = guid_pro + "_" + files[0].name;
                    document.getElementById('<%= T_NOMBRE_ARCHIVO_REAL.ClientID %>').value = files[0].name;
                });
            }
            else {
                alert("ERROR AL SUBIR ARCHIVO");
            }
        }

        function SubirDocumento3() {
            var data = new FormData();
            var files = $("#FU_LOGO3").get(0).files;
            var guid_pro = guid();
            if (files.length > 0) {
                var id_camion = document.getElementById('<%= T_ID_CAMION.ClientID %>').value;
                data.append("UploadedImage", files[0]);
                var ajaxRequest = $.ajax({
                    type: "POST",
                    url: "SUBE_ARCHIVOS.aspx?tipo_doc=1&id_camion=" + id_camion + "&guid=" + guid_pro,
                    contentType: false,
                    processData: false,
                    data: data
                });
                ajaxRequest.done(function (xhr, textStatus) {
                    $('#L_UP3').html("<span class='label label-success' style='font-size:12px;'><i class='fa fa-check'></i> Subido con éxito, por favor ahora guarde.</span>");
                    document.getElementById('<%= T_REV_NOMBRE_BD.ClientID %>').value = guid_pro + "_" + files[0].name;
                    document.getElementById('<%= T_REV_NOMBRE_REAL.ClientID %>').value = files[0].name;
                });
            }
            else {
                alert("ERROR AL SUBIR ARCHIVO");
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


        function CerrarModalMant() {
            $('#CERRAR_MODAL2').click();
        }
        function CerrarModalMant3() {
            $('#CERRAR_MODAL3').click();
        }
        function CerrarModalMant4() {
            $('#CERRAR_MODAL4').click();
        }

        function VerArchivo2(archivo, id_camion) {
            var win = window.open('Documentos/Camiones/MANT/' + id_camion + '/' + archivo, '_blank');
            win.focus();
        }

        function Verrevtecnica() {
            var id_camion = document.getElementById('<%= T_ID_CAMION.ClientID %>').value;
            var ruta_archivo = document.getElementById('<%= T_REV_NOMBRE_BD.ClientID %>').value
            if (ruta_archivo == "" || ruta_archivo == "Aun no se carga el documento") {
                MostrarNotificacion('Aun no se carga documento de revisión técnica', 0);
            }
            else {
                var win = window.open('Documentos/Camiones/DOCS/' + id_camion + '/' + ruta_archivo, '_blank');
                win.focus();
            }

        }

      <%--  function verdoc1() {
            var id_doc = document.getElementById('<%= T_DET_DOC.ClientID %>').value;
            var archivo = document.getElementById('<%= T_DOC_ACTUAL1.ClientID %>').value;
            var win = window.open('Documentos/Camiones/DOCS/' + id_doc + '/' + archivo, '_blank');
            win.focus();
        }--%>

        <%--function verdoc2() {
            var id_mant = document.getElementById('<%= T_ID_MANTENCION.ClientID %>').value;
            var archivo = document.getElementById('<%= T_DOC_ACTUAL2.ClientID %>').value;
            var win = window.open('Documentos/Camiones/MANT/' + id_mant + '/' + archivo, '_blank');
            win.focus();
        }--%>
</script>
</asp:Content>
