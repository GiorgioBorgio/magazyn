   M S B A M L   `   `   `  ����B  >Magazyn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:MagazynMagazyn  5   	       Magazyn.AddUser   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.7.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   !localclr-namespace:Magazyn  5   	     � WindowStartupLocation$  CenterScreen=�5	   	    � Title$ Dodaj użytkownika��5
   	   $	��673��6#   $	��450��60    � 
ResizeMode$ NoResize=�6<   ���#f2f2f2 � 	Resources 5         PrimaryColor �� Color  SecondaryColor  TextBoxBackground  BorderColor �� 
TargetType �� Property �� Value%  Xceed.Wpf.Toolkit.MaskedTextBox(  "System.Windows.Controls.DatePicker��%�  &	        &	 !     &	 B     &	 a     '��      7  '�� 2    7  7  '  $    7  7  '      '�� �    7   '	� �    �� 5   
   $ #4CAF50��6/   �� 5   
   $ #333333��61   �� 5   
   $ White��64   �� 5   
   $
 #ccc��6.   �� 5   
   " ��6   .�5      ��  � ��$ 14(�6)   �� 5       � ��8   6+   �� 5       � ��$ 5q�6'   �� 5       � ��$ Center=�62   5      �� 5   
   " ��6   .�5      ��  � ��$ 5q�6'   �� 5       � ��$ 5q�6(   �� 5       � ��$ 1q�60   �� 5        � ��8   6,   �� 5!       � ��8  6+   �� 5"       � ��$	 200��6&   5#      �� 5%   
   "  6   .�5&      ��  � ��$ 5q�6'   �� 5'       � ��$ 5q�6(   �� 5(       � ��$ 1q�60   �� 5)       � ��8   6,   �� 5*       � ��8  6+   �� 5+       � ��$	 200��6&   5,      �� 5/   
   "  6   .�50      ��  � ��$ 5q�6'   �� 51       � ��$	 200��6&   52      �� 55   
   " ��6   .�56      ��  � ��8   6+   �� 57       � ��
 �����6+   �� 58       � ��$ 16(�6)   �� 59       � ��$
 Bold"�6+   �� 5:       � ��$ 0q�60   �� 5;       �@�Cursor$
 Hand��6'   �� 5<       � ��$ 5q�6'   �� 5=       � ��$ 45��6'   �� 5>       � ��$	 126��6&   5?      �� 5B   
   " 	�6   .�5C      ��  � ��$ 5,0,5,0q�6'   5D      5E      .��5G      � $��
10,10,10,6q�6    � RowDefinitions 5I   
   �� 5J      $
��Auto �6   �� 5L      $
��Auto �6   �� 5N      $
��Auto �6   �� 5P      $
��Auto �6   �� 5R      $
��Auto �6   �� 5T      $
��Auto �6   �� 5V      $
��Auto �6   �� 5X      $
��Auto �6   �� 5Z      $
��Auto �6   �� 5\      $
��Auto �6   �� 5^      $
��Auto �6   �� 5`      $
��Auto �6   �� 5b      $
��Auto �6   �� 5d      $��* �6   �� 5f      $
��Auto �6   	 � ColumnDefinitions	 5k   
   �� 5l      $
��Auto �6   �� 5m      $��* �6   .+�5q   
   �� $��Login��6   $��0��6    $��0��6-   �� 5r   
   -   5        
 ��Name$
 Textbox_login��5r      $��0��6)   $��1��66   �� 5u   
   $��Imię��6   $��1��6   $��0��6,   �� 5v   
   -   5        $
 Textbox_imie��5v      $��1��6(   $��1��65   6E   �� 5y   
   $��Nazwisko��6   $��2��6#   $��0��60   �� 5z   
   -   5        $
 Textbox_nazwisko��5z      $��2��6,   $��1��69   6I   �� 5}   
   $��Miejscowość��6   $��3��6&   $��0��63   �� 5~   
   -   5        $
 Textbox_miejscowość��5~      $��3��6/   $��1��6<   6L   �� 5�   
   $��Kod pocztowy��6   $��4��6'   $��0��64     5�   
   -   5          Name$ Textbox_kod_pocztowy��5�      $��4��6;   $��1��6H      Mask$ 00-000��6X      
PromptChar$ _��6f   $��1��6u   �� 5�   
   $��Ulica (opcjonalnie)��6   $��5��6.   $��0��6;   �� 5�   
   -   5        $
 Textbox_ulica��5�      $��5��6)   $��1��66   6F   �� 5�   
   $��Numer posesji��6   $��6��6(   $��0��65   �� 5�   
   -   5        $
 Textbox_numer_posesji��5�      $��6��61   $��1��6>   �� 5�   
   $��Numer lokalu (opcjonalne)��6   $��7��64   $��0��6A   �� 5�   
   -   5        $
 Textbox_numer_lokalu��5�      $��7��60   $��1��6=   �� 5�   
   $��Pesel��6   $��8��6    $��0��6-   �� 5�   
   -	   5        $
 Textbox_pesel��5�       �� 	MaxLength$ 11��6)   $��8��6h   $��1��6u   �� 5�   
   $��Data urodzenia��6   $��9��6)   $��0��66     5�   
   -
   5          Name$  Date_picker_data_urodzenia��5�      $��9��6:   $��1��6G   $��Center=�6W   $��Center=�6t   �� 5�   
   $��Płeć��6   $��10��6   $��0��6-   �� 5�   
   $��10��6   $��1��6#    �� Orientation$ 
Horizontal=�63   $��Center=�6L   .+�5�      	� -   5         	�Name$ Radio_btn_kobieta��5�      $��Kobieta��65   	� 5�      -   5        $ Radio_btn_mężczyzna��5�      $��Mężczyzna��67    d� 	IsChecked$
 TrueY�6K   5�      �� 5�   
   $
��Mail��6   $��11��6   $��0��6-   �� 5�   
   -   5        $
 Textbox_mail��5�      $��11��6(   $��1��66   �� 5�   
   $��Numer telefonu��6   $��12��6)   $��0��67   �� 5�   
   -   5        $
 Textbox_numer_telefonu��5�      $ 9��62   $��12��6p   $��1��6~   �� 5�   
   $��13��6   $��0��6#   $��2��63   $ 
Horizontal=�5�      $��Center=�5�      .+�5�      �� -   5        $��Dodaj��5�      $	��140��5�      $��45��5�      5�      �� 5�      -   5        $��Anuluj��5�      $	��140��5�      $��45��5�      ���#94A3B85�      5�      5�      5�      