   M S B A M L   `   `   `  ����O  KPoisson_distribution, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Poisson_distributionPoisson_distribution  5   	   %    Poisson_distribution.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Poisson_distribution     5   	     � Title$  
MainWindow��5   	   $	��460��6   $	��300��6)   $	��300��65   $	��460��6D    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ 	CanResize=�5
   	    � WindowStyle$ SingleBorderWindow=�6     � 	Resources 5         FocusVisual �� Property �� Value �� StrokeDashArray J� SnapsToDevicePixels  Button.Static.Background	 �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 RadiusButton
 �� 
TargetType �� 
TargetType ��Name �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName  FocusVisual1  Button.Static.Background1  Button.Static.Border1   Button.MouseOver.Background1  Button.MouseOver.Border1  Button.Pressed.Background1  Button.Pressed.Border1  Button.Disabled.Background1  Button.Disabled.Border1  Button.Disabled.Foreground1  RadiusButton2��%.  &	        &	 �     &	 �     &	 �     &	     &	 ?    &	 b    &	 �    &	 �    &		 �    &	
 �    7   7  7  &	 �    &	 P    &	 s    &	 �    &	 �    &	 �    &	 �    &	 "    &	 E    &	 h    &	 �    7  7  7  �� 5   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$   $	 1 2R�6/   #��� ��6E   $
 true��6�   $��1��6�   5      5      5      �� 5   
   $	 	#FFDDDDDD��6;   �� 5   
   $	 	#FF707070��67   �� 5   
   $	 	#FFBEE6FD��6>   �� 5   
   $	 	#FF3C7FB1��6:   �� 5   
   $	 	#FFC4E5F6��6<   �� 5   
   $	 	#FF2C628B��68   �� 5   
   $	 	#FFF4F4F4��6=   �� 5   
   $	 	#FFADB2B5��69   �� 5   
   $	 	#FF838383��6=   �� 5   
   "
 ��6%   .�5      ��  �@�FocusVisualStyle8   61   �� 5        � ��8  6+   �� 5!       � ��8  6,   �� 5"       � ��# � ��6+   �� 5#       � ��$ 1q�60   �� 5$       � ��$ Center=�6;   �� 5%       � ��$ Center=�69   �� 5&       � ��$ 1q�6(   �� 5'       � �� 5(      �� 5)      " ��6&   .R�5*      �� $ border��6!   $ 25��61   #��z��6C   #��z��6m   #��z��6�   $
 true��6�   ._�5+      �� $ contentPresenter��6/   $|�False��6I   #��z��6[   #��z��6�   $
��True��6�   # z 6�   #��z��6  5,       5-      P� 5.       �@��IsDefaulted$
 true��6=   . �5/   "   �� $ border��6@   # � ��6T    � ��50      P� 51       � ��$
 true��6;   . �52   "   �� $ border��6?    � �� �#E36C0B6S   �� 53   "   $ border��6@    � �� �#E36C0B6T   54      56      58      59      �� 5:   
   .�5;      ��  � �� 5<      �� 5=      .R�5>      �� $��2q�6$   $	 1 2R�6/   #��� ��6E   $
 true��6�   $��1��6�   5?      5A      5B      �� 5C   
   $	 	#FFDDDDDD��6<   �� 5D   
   $	 	#FF707070��68   �� 5E   
   $	 	#FFBEE6FD��6?   �� 5F   
   $	 	#FF3C7FB1��6;   �� 5G   
   $	 	#FFC4E5F6��6=   �� 5H   
   $	 	#FF2C628B��69   �� 5I   
   $	 	#FFF4F4F4��6>   �� 5J   
   $	 	#FFADB2B5��6:   �� 5K   
   $	 	#FF838383��6>   �� 5L   
   "
 ��6&   .�5M      ��  �@�FocusVisualStyle8   61   �� 5N       � ��8  6+   �� 5O       � ��8  6,   �� 5P       � ��# � ��6+   �� 5Q       � ��$ 1q�60   �� 5R       � ��$ Center=�6;   �� 5S       � ��$ Center=�69   �� 5T       � ��$ 1q�6(   �� 5U       � �� 5V      �� 5W      " ��6&   .R�5X      �� $ border��6!   $ 25��61   #��z��6C   #��z��6m   #��z��6�   $
 true��6�   ._�5Y      �� $ contentPresenter��6/   $|�False��6I   #��z��6[   #��z��6�   $
��True��6�   # z 6�   #��z��6  5Z       5[      P� 5\       �@��IsDefaulted$
 true��6=   . �5]   "   �� $ border��6@   # � ��6T    � ��5^      P� 5_       � ��$
 true��6;   . �5`   "   �� $ border��6?    � �� �#E36C0B6S   �� 5a   "   $ border��6@    � �� �#E36C0B6T   5b      5d      5f      5g      5h      .��5i      � ���	#FFE3CCCC$��0,0,0,-6q�6"   $	��408��64   $
��Auto��6D    � RowDefinitions 5j   
   �� 5k      $	��35* �6   �� 5l      $
��129* �6   �� 5m      $	��45* �6   �� 5n      $	��35* �6   �� 5o      $	��45* �6   �� 5p      $
��128* �6   �� 5q      $	��40* �6    � ColumnDefinitions 5s   
   �� 5u      $	��77* �6   �� 5v      $
��141* �6   �� 5w      $	��82* �6   .+�5z   
   �� -   5        #��� 
 5z      $��Start Simulation��68   $
��Auto��6S   $
��Auto��6b   $	��200��6o   $��4��6�   $��1��6�   ���	#FFF77348
����������	#FFF77348$��Bodoni MT Black)�6�   �� 5{   
   -   5        #���  5{      $��	Your Data��69   $
��Auto��6M   $
��Auto��6[   $	��200��6h   $��1��6�   $��2��6�   ���	#FFF77348
����������	#FFF77348$��Bodoni MT Black)�6�   �� 5|   
   $
��Auto��6   $
��Auto��6"   ���	#FFF77348���	#FFF77348$��3��6S   �� 5}   
   $
��Auto��6   $
��Auto��6"   ���	#FFF77348���	#FFF77348$��3��6S   $��6��6g   �� 5~   
   $2��,Created exclusively for educational purposes��6   $��Center=�6G   $��Center=�6k   $��6��6�   $��Normal&�6�   $��3��6�   $��0,0,0,10q�6�   $��Bodoni MT Black)�6�   ���	#FFFCFCFC6�   5�      5�      