   M S B A M L   `   `   `  ����O  KFlexInterfaceManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:FlexInterfaceManagerFlexInterfaceManager  5      G(clr-namespace:FlexInterfaceManager.ModelFlexInterfaceManager.Model  5          FlexInterfaceManager.App   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      5local2"clr-namespace:FlexInterfaceManager     5      :local(clr-namespace:FlexInterfaceManager.Model     5        �� 	Resources  5         DatePickerStyle �� 
TargetType(  "System.Windows.Controls.DatePicker �� Property �� Value  ComboxStyle  TextblockStyle  StackPanelStyle   StackPanelStyle_CustomerFund  FundTemplate �� DataType2   ,FlexInterfaceManager.Model.AccountSelectData �� Orientation d� 	IsChecked �� Mode  QueryButton �� Triggers	 ]� 
Conditions
 �� Property �� Value5   /FlexInterfaceManager.Model.TopAccountGroupModel �� ItemsSource2   ,FlexInterfaceManager.Model.AccountGroupModel-   'FlexInterfaceManager.Model.AccountModel  ValidateStyle_TextBox P� Property P� Value �� RelativeSource �� Path+   %FlexInterfaceManager.ViewModelLocator  ViewModelLocator��%�	  &	        &	 �     &	 �    &	     &	 �    &	 R    &	     (�� �    5>   #   *, +)(�� t    5D   #   *, +)(�� I    5J      *, +)&	     &	 �    �� 5	   
   "  6(   .�5
      ��  � ��$ 25��6'   64   �� 5       � ��$	 200��6&   64   �� 5       � ��$ Center=�62   6C   5      �� 5   
   " ��6$   .�5      ��  � ��$ 30��6'   64   �� 5       � ��$	 200��6&   64   �� 5       � ��$ Center=�62   6C   �� 5      % �@��IsSynchronizedWithCurrentItem$
 TrueY�6>   6M   5      �� 5   
   " ��6'   .�5      ��  � ��$	 110��6&   64   �� 5       � ��$ Center=�62   6C   5      �� 5   
   " ��6(   .�5      ��  �@��Orientation$ 
Horizontal=�6,   6A   �� 5       � ��$	 150��6'   65   �� 5        � ��$ Center=�62   6C   5!      �� 5#   
   " ��65   .�5$      ��  �@��Orientation$ 
Horizontal=�6,   6A   �� 5&       � ��$ Center=�62   6C   5'      �� 5)   
   "  6,   .R�5*      �� $ 
Horizontal=�6   $��10q�63   .+�5+      ��  6   �� *	IsChecked+$ TwoWay=�6K   �� 5,      ��6   �� *Code+64   5-      5.      �� 5/   
   " ��6$   .�50      ��  � ��$ 0,5,20,5q�6'   6:   �� 51       � ��$ 30��6*   67   �� 52       � ��$ 75��6&   63   �� 53       � ��$
 Left=�64   6C    54      ]� 55      	 56      �� 57      
 �@J�IsMouseOver . 6J   �� 58      
 � �� .  6I   ./�5:      ��  � �� �	#FF809FDC6G   5;      5=      �� 5>   
   "  6#    6R   �� *AccountGroups+.R�5?      �� $ 
Horizontal=�6   .+�5@      ��  6   �� *	IsChecked+$ TwoWay=�6J   �� 5A      ��6   �� *Code+64   5B      5C      �� 5D   
   "  6#    6O   �� *
Accounts+.R�5E      �� $ 
Horizontal=�6   .+�5F      ��  6   �� *	IsChecked+$ TwoWay=�6J   �� 5G      ��6   �� *Code+64   5H      5I      �� 5J   
   "  6   .R�5K      �� $ 
Horizontal=�6   .+�5L      ��  6   �� *	IsChecked+$ TwoWay=�6J   �� 5M      ��6   �� *ComposedCode+6<   5N      5O      �� 5P   
   " ��6.    5Q      P� 5R       �@=�HasError .  �5S      �� 5T       �@�ToolTip 65   ��  �� *Self+$) #(Validation.Errors)[0].ErrorContent�5V      5X      5Z        5\   
   6=   5]      5^      