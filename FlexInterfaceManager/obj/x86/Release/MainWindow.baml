   M S B A M L   `   `   `  ���� O  KFlexInterfaceManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:FlexInterfaceManager.ModelFlexInterfaceManager.Model  ;"clr-namespace:FlexInterfaceManagerFlexInterfaceManager  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem I EAdornedControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:AdornedControl;assembly=AdornedControlAdornedControl %    FlexInterfaceManager.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     :local(clr-namespace:FlexInterfaceManager.Model     5local2"clr-namespace:FlexInterfaceManager     6sys&clr-namespace:System;assembly=mscorlib     Cac4clr-namespace:AdornedControl;assembly=AdornedControl     -   $��Main_Window��  � Title$  iExchangeToFlex�����	#FFD4D0C8$��Stretch=�$��Stretch=�$	��700��$	��850�� � WindowState$ 	Maximized=�.��#  AdornedControl.AdornedControl  -   $��LoadingAdorner��   IsAdornerVisible .     AdornerContent &    FlexInterfaceManager.LoadingWait  .��� -   $��gridMain��$��20,10,20,20q� � RowDefinitions �� $��30 ��� �� $��70 �.+�� $	��Top=�$��1��$��	10,5,5,10q� � ColumnDefinitions �� �� .+��� ���	#FF808080$��1q�$��0��$��10,8,5,2q� �� Padding$ 10,0,0,0q�._��  �� $
��auto ��� $	��250���� $
��auto ��� $	��200��.+��� $��Stretch=�$��Stretch=�$��1��$��0��$��Account��$��0,0,0,10q�.���  �� $
��auto ��� .+�� $��1��.+�R� -   $��
lbCustomer��$y�Visible=����� *AccountGroupCol+����  �� 
TargetType" Q�.���  �� Property �@Q�
IsExpanded	 �� Value	 �� *
IsExpand+
 �� Mode$
 TwoWay=��� -   $��spFund��    StackPanelStyle_CustomerFund#��[  $��2��.+��� $��FundNoSelectType:��  TextblockStyle#��[ �� -   $��cbSelectFundType��  ComboxStyle#��[  �� SelectedIndex$ 0������ *FundSelectView+�� -   $��gbFund��$��Stretch=�$��Stretch=�$��3��$��0��$��FundNo��.��� .+��� -   $��spFundByRange��$y�	Collapsed=�.+���  �� Orientation$ 
Horizontal=�$��0,0,0,30q�.+��� $��From:��#��[ �� -	   $��
cbFundFrom��#��[  �� 
IsEditable . $��1�� �� DisplayMemberPath$
 Code�� �� SelectedValuePath$
 Code������ *FromSelectView_Fund+�� $ 
Horizontal=�.+��� $	��To:��#��[ $��2���� -
   $��cbFundTo��#��[  . $��3��$
 Code��$
 Code������ *ToSelectView_Fund+� -   $��gridFund��$y�	Collapsed=� �� $
��auto ��� .+��� $ 
Horizontal=�$��8,8,0,0q�$��30��.+��� -   $��	cbFundAll��$	��100��.��All�� ���� *FundStatusText+�� $��1��  FundTemplate#��[ ���� *	FundCol+�� ���	#FF808080$��1q�$��1��$��10,8,5,2q�$ 10,0,0,0q�._��  �� $	��100 ��� $	��100 ��� $	��100 ��� $	��100 ��� .+���   StackPanelStyle#��[ $��0��.+��� #��[ $��FromTradeDay:��(  "System.Windows.Controls.DatePicker  -   $��dpFromTradeDay��  DatePickerStyle#��[    SelectedDate �� *FromTradeDay+$
 TwoWay=��� #��[ $��1��.+��� #��[ $��ToTradeDay:��  -   $��dpToTradeDay��#��[  �� *
ToTradeDay+$
 TwoWay=��� #��[ $��2��.+��� $��Type:��#��[ �� -   $��cbDepositType��#��[ $��0��$��5��$ 0������ *BusinessTypeView+�� #��[ $��3��.+��� $��QueryFundNo:��#��[ �� -   $��QueryFundBtn��$��30��$	��100��$��Query������ *QueryFundCmd+�� -   $��gbUser��$	��500��$��Stretch=�$��Stretch=�$��4��$��2��$
��User��.���  �� $
��auto ��� .+��� ���	#FF808080$��1q�._��  �� $	��100 ��� $	��100 ��� $	��100 �.+���   QueryButton#��[ $��0��$
��Left=�$	��Add������ *
AddUserCmd+�� #��[ $��1��$
��Left=�$��Delete������ *
DelUserCmd+�� #��[ $��2��$
��Left=�$
��Edit������ *SaveUserCmd+&   System.Windows.Controls.DataGrid  $��1��$ �� IsSynchronizedWithCurrentItem$
 TrueY�   AutoGenerateColumns .     CanUserAddRows .  ���� *
UserView+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ LoginID��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 150    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	LoginID+  $ LastModifiedDate��$	 150  .  �� *LastModifiedDate+�� -   $��DepositSearchBtn��$��2��$��2��$��	0,20,0,20q�$��30��$	��150��$��Export������ *	ExportCmd+�� $��0��.@��� .���� -   $��	statusTxt��0	   *FlexInterfaceManager.Model.NonTopmostPopup	  -    	 Name$ popupProgressMessage��$y�	Collapsed=� � AllowsTransparency . ��.  $��Center=� � DataContext �� *StartUpViewModel+  ViewModelLocator �� Source# [  � PlacementTarget ��  �� ElementName$ Main_Window��.���� $	��400��$��50��
�������.+�� -   $��progressBar1��$��21��$��Center=�$��	12,33,0,0q�$��Center=�$	��373�� �� Left$ 0��
  �� Top$	  -16��! � Value! �� *ProgessValue+�� -   $��textBlockProgressMessage��$��23��$��Center=�$��Center=�" �� TextAlignment$" Center=����� *	RunInfo+	  -   $ 
PopupLogin�� . ��. $��Center=� �� *LoginViewModel+# [  �� $ Main_Window��.���� # �� CornerRadius$# 10��
�������$��3q�
�������$��113,39,71,26q�$ 4q�$ J� Effect$ 3
  -System.Windows.Media.Effects.DropShadowEffect
  % 
  Color$
% Gray��& 
  Opacity$	& .50Q�' 
  ShadowDepth$' 16Q�._��� $	��308��$	��217��.+��� -   $��
textBlock1��$��23��$��
0,14,45,14q�$��1��$��Name:��$ 70��$  39���� -   $��tbLoginName��$��23��$��1��$��1��$	��120��  ValidateStyle_TextBox#��[ $��1��$	 124��$  53������ *Name+$
 TwoWay=�( �� ValidatesOnDataErrors( . ) �� NotifyOnValidationError) . �� -   $��
textBlock2��$��23��$��
0,14,27,14q�$��	Password:��$��2��$ 70��$  79��G� -   $	��pwb��$��2��$	 124��$  93��$��23��$	��120��$��1��$��2���� -   $��button1��$��Login��$��23��$��64��* �� 	IsDefault* . $��3��$��3��$ 70��$	  138������ *
LoginCmd+���� $	 pwb���� -   $��button2��$��Cancel��$��23��$��64��$��4��$	 180��$	  138������ *	CancelCmd+