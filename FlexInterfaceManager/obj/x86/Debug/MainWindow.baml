   M S B A M L   `   `   `  ����O  KFlexInterfaceManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:FlexInterfaceManager.ModelFlexInterfaceManager.Model  5   	   ;"clr-namespace:FlexInterfaceManagerFlexInterfaceManager  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   I EAdornedControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:AdornedControl;assembly=AdornedControlAdornedControl 5   	   %    FlexInterfaceManager.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   :local(clr-namespace:FlexInterfaceManager.Model     5   	   5local2"clr-namespace:FlexInterfaceManager     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   Cac4clr-namespace:AdornedControl;assembly=AdornedControl     5   	   -   5        $��Main_Window��5	   !     � Title$  iExchangeToFlex��6	   ���	#FFD4D0C8$��Stretch=�6L   $��Stretch=�6h   $	��700��6�   $	��850��6�    � WindowState$ 	Maximized=�6�   .��5      #  AdornedControl.AdornedControl  -   5        $��LoadingAdorner��5         IsAdornerVisible .     AdornerContent 5   
   &    FlexInterfaceManager.LoadingWait  5      6#   .��5   
   � -   5        $��gridMain��5      $��20,10,20,20q�6!    � RowDefinitions 5      �� 5      $��30 �6"   60   �� 5      6#   �� 5      $��70 �6    6/   .+�5      � $	��Top=�6   $��1��6+   $��	10,5,5,10q�68    � ColumnDefinitions 5      �� 5      6)   �� 5      6*   .+�5      �� ���	#FF808080$��1q�61   $��0��6E   $��10,8,5,2q�6S    �� Padding$ 10,0,0,0q�6e   ._�5      �  5       �� 5!      $
��auto �6-   6=   �� 5"      $	��250��6,   6?   �� 5#      $
��auto �6-   6>   �� 5$      $	��200��6-   6@   .+�5'      �� $��Stretch=�6#   $��Stretch=�6?   $��1��6^   $��0��6k   $��Account��6|   $��0,0,0,10q�6�   .��5(      �  5)   "   �� 5*   &   $
��auto �64   6D   �� 5+   &   66   .+�5.   "   � $��1��6'   .+�5/   &   R� -   5        $��
lbCustomer��5/   3   $y�Visible=�6E   ��6Z   �� *AccountGroupCol+��50   *   �� 51   .    �� 
TargetType" Q�64   .�52   2   ��  �� Property �@Q�
IsExpanded	 �� Value	 6O   �� *
IsExpand+
 �� Mode$
 TwoWay=�53   /   56   '   59   #   5:      5>      �� 5?      -   5        $��spFund��5?   j       StackPanelStyle_CustomerFund#��[  6&   $��2��6]   .+�5@      �� $��FundNoSelectType:��6(     TextblockStyle#��[ 6A   6k   �� 5A      -   5        $��cbSelectFundType��5A   L     ComboxStyle#��[ 6'    �� SelectedIndex$ 0��6d   ��6v   �� *FundSelectView+6�   5B      �� 5C      -   5        $��gbFund��5C   �   $��Stretch=�6$   $��Stretch=�6@   $��3��6^   $��0��6k   $��FundNo��6{   .��5D      � .+�5E   "   �� -   5        $��spFundByRange��5E   D   $y�	Collapsed=�6-   .+�5F   &   ��  �� Orientation$ 
Horizontal=�61   $��0,0,0,30q�6J   .+�5G   *   �� $��From:��64   #��[ 6A   6k   �� 5H   *   -	   5        $��
cbFundFrom��5H   z   #��[ 63    �� 
IsEditable . $��1��6j    �� DisplayMemberPath$
 Code��6�    �� SelectedValuePath$
 Code��6�   ��6�   �� *FromSelectView_Fund+6�   5I   '   �� 5J   &   $ 
Horizontal=�61   .+�5K   *   �� $	��To:��64   #��[ 6?   $��2��6g   6y   �� 5L   *   -
   5        $��cbFundTo��5L   {   #��[ 63    . $��3��6k   $
 Code��6�   $
 Code��6�   ��6�   �� *ToSelectView_Fund+6�   5M   '   5N   #   � 5P   "   -   5        $��gridFund��5P   '   $y�	Collapsed=�67    5Q   &   �� 5R   *   $
��auto �68   6I   �� 5S   *   6;   .+�5U   &   �� $ 
Horizontal=�61   $��8,8,0,0q�6J   $��30��6[   .+�5V   *   �� -   5        $��	cbFundAll��5V   ?   $	��100��63   .��6�   All6�   �� 5W   *   ��64   �� *FundStatusText+6V   5X   '   �� 5Y   &   $��1��61     FundTemplate#��[ 6`   ��6@   �� *	FundCol+5[   '   5]   #   5^      5`      5a      5b      �� 5c      ���	#FF808080$��1q�61   $��1��6E   $��10,8,5,2q�6U   $ 10,0,0,0q�6g   ._�5d      �  5e      �� 5f      $	��100 �6,   6;   �� 5g      $	��100 �6,   6;   �� 5h      $	��100 �6,   6;   �� 5i      $	��100 �6-   6<   �� 5j      6.   .+�5l      ��   StackPanelStyle#��[ 6&   $��0��6O   .+�5m      �� #��[ 6(   $��FromTradeDay:��6Q   6i   (  "System.Windows.Controls.DatePicker  5n      -   5        $��dpFromTradeDay��5n   T     DatePickerStyle#��[ 6*      SelectedDate 6l   �� *FromTradeDay+$
 TwoWay=�5o      �� 5p      #��[ 6&   $��1��6O   .+�5q      �� #��[ 6(   $��ToTradeDay:��6Q   6h     5r      -   5        $��dpToTradeDay��5r   T   #��[ 6*    6h   �� *
ToTradeDay+$
 TwoWay=�5s      �� 5t      #��[ 6&   $��2��6P   .+�5u      �� $��Type:��6)   #��[ 66   6a   �� 5v      -   5        $��cbDepositType��5v   M   #��[ 6'   $��0��6b   $��5��6o   $ 0��6�   ��6�   �� *BusinessTypeView+6�   5w      �� 5x      #��[ 6%   $��3��6N   .+�5y      �� $��QueryFundNo:��6)   #��[ 6=   6h   �� 5z      -   5        $��QueryFundBtn��5z   n   $��30��6%   $	��100��61   $��Query��6=   ��6M   �� *QueryFundCmd+6�   5{      �� 5|      -   5        $��gbUser��5|   m   $	��500��6#   $��Stretch=�63   $��Stretch=�6O   $��4��6{   $��2��6�   $
��User��6�   .��5}      �  5~   "   �� 5   &   $
��auto �64   6D   �� 5�   &   66   .+�5�   "   �� ���	#FF808080$��1q�6A   ._�5�   &   �  5�   *   �� 5�   .   $	��100 �6@   6N   �� 5�   .   $	��100 �6?   6M   �� 5�   .   $	��100 �6?   6M   .+�5�   *   ��   QueryButton#��[ 61   $��0��6V   $
��Left=�6f   $	��Add��6�   ��6�   �� *
AddUserCmd+6�   �� 5�   *   #��[ 61   $��1��6V   $
��Left=�6f   $��Delete��6�   ��6�   �� *
DelUserCmd+6�   �� 5�   *   #��[ 61   $��2��6V   $
��Left=�6f   $
��Edit��6�   ��6�   �� *SaveUserCmd+6�   5�   '   5�   #   &   System.Windows.Controls.DataGrid  5�   "   $��1��6+   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6Y      AutoGenerateColumns .     CanUserAddRows .  ��68   �� *
UserView+   Columns 5�   &   0  *System.Windows.Controls.DataGridTextColumn  5�   *   ,  &System.Windows.Controls.DataGridColumn   Header$ LoginID��6Y      Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 6j      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *	LoginID+6�     5�   *   $ LastModifiedDate��6b   $	 150 6|    .  6=   �� *LastModifiedDate+6�   5�   #   5�      5�      5�      5�      5�      �� 5�      -   5        $��DepositSearchBtn��5�   L   $��2��6   $��2��6)   $��	0,20,0,20q�67   $��30��6e   $	��150��6q   $��Export��6~   ��6�   �� *	ExportCmd+6�   �� 5�      $��0��6   .@�5�      �� .��5�      �� -   5        $��	statusTxt��5�       63   5�      5�      0	   *FlexInterfaceManager.Model.NonTopmostPopup	  5�      -   5         	 Name$ popupProgressMessage��5�   $   $y�	Collapsed=�6B    � AllowsTransparency . ��.  $��Center=�6   � DataContext 6Y   �� *StartUpViewModel+  ViewModelLocator �� Source# [  � PlacementTarget 6�   ��  �� ElementName$ Main_Window��.��5�      �� $	��400��6   $��50��6%   
�������.+�5�      � -   5        $��progressBar1��5�   ^   $��21��6"   $��Center=�6.   $��	12,33,0,0q�6K   $��Center=�6r   $	��373��6�    �� Left$ 0��6�   
  �� Top$	  -16��6�   ! � Value! 6�   �� *ProgessValue+6�   �� 5�      -   5        $��textBlockProgressMessage��5�   K   $��23��6    $��Center=�6,   $��Center=�6�   " �� TextAlignment$" Center=�6�   ��6k   �� *	RunInfo+6�   5�      5�      	  5�      -   5        $ 
PopupLogin��5�   $    . ��. $��Center=�6�    6S   �� *LoginViewModel+# [  6�   �� $ Main_Window��.��5�      �� # �� CornerRadius$# 10��6   
�������$��3q�6@   
�������$��113,39,71,26q�6g   $ 4q�6}   $ J� Effect$ 5�      3
  -System.Windows.Media.Effects.DropShadowEffect
  5�      % 
  Color$
% Gray��6+   & 
  Opacity$	& .50Q�68   ' 
  ShadowDepth$' 16Q�6F   ._�5�      �� $	��308��6   $	��217��6)   .+�5�      �� -   5        $��
textBlock1��5�   T   $��23��6$   $��
0,14,45,14q�60   $��1��6D   $��Name:��6f   $ 70��6v   $  39��6�   �� 5�      -   5        $��tbLoginName��5�   O   $��23��6#   $��1��6/   $��1��6<   $	��120��6d     ValidateStyle_TextBox#��[ 6p   $��1��6�   $	 124��6	  $  53��6  ��6�   �� *Name+$
 TwoWay=�( �� ValidatesOnDataErrors( . ) �� NotifyOnValidationError) . 6-  �� 5�      -   5        $��
textBlock2��5�   H   $��23��6$   $��
0,14,27,14q�61   $��	Password:��6Z   $��2��6m   $ 70��6z   $  79��6�   G� 5�      -   5        $	��pwb��5�   a   $��2��6&   $	 124��63   $  93��6E   $��23��6U   $	��120��6m   $��1��6y   $��2��6�   �� 5�      -   5        $��button1��5�   D   $��Login��6!   $��23��63   $��64��6U   * �� 	IsDefault* . $��3��6�   $��3��6�   $ 70��6�   $	  138��6�   ��6b   �� *
LoginCmd+��6�   �� $	 pwb��6�   �� 5�      -   5        $��button2��5�   K   $��Cancel��6!   $��23��64   $��64��6@   $��4��6y   $	 180��6�   $	  138��6�   ��6Z   �� *	CancelCmd+6�   5�      5�      5�      5�      5�      5�      