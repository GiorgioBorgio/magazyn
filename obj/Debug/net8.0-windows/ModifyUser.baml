   M S B A M L   `   `   `  ����B  >Magazyn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:MagazynMagazyn  5   	       Magazyn.ModifyUser   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Magazyn     5   	   X TXceed.Wpf.Toolkit, Version=4.7.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  6-     � Title$  Edytuj użytkownika��5   	   $	��673��6$   $	��450��61    � 
ResizeMode$ NoResize=�6=    � WindowStartupLocation$ CenterScreen=�5	   	   ���#f2f2f2 � 	Resources 5         PrimaryColor �� Color  SecondaryColor  TextBoxBackground  BorderColor �� 
TargetType �� Property �� Value%  Xceed.Wpf.Toolkit.MaskedTextBox(  "System.Windows.Controls.DatePicker��%�  &	        &	 !     &	 B     &	 a     '��      7  '�� 2    7  7  '  $    7  7  '      '�� �    7   '	� �    �� 5   
   $ #4CAF50��6/   �� 5   
   $ #333333��61   �� 5   
   $ White��64   �� 5   
   $
 #ccc��6.   �� 5   
   " ��6   .�5      ��  � ��$ 14(�6)   �� 5       � ��8   6+   �� 5       � ��$ 5q�6'   �� 5       � ��$ Center=�62   5      �� 5   
   " ��6   .�5      ��  � ��$ 5q�6'   �� 5       � ��$ 5q�6(   �� 5       � ��$ 1q�60   �� 5       � ��8   6,   �� 5        � ��8  6+   �� 5!       � ��$	 200��6&   5"      �� 5$   
   "  6   .�5%      ��  � ��$ 5q�6'   �� 5&       � ��$ 5q�6(   �� 5'       � ��$ 1q�60   �� 5(       � ��8   6,   �� 5)       � ��8  6+   �� 5*       � ��$	 200��6&   5+      �� 5.   
   "  6   .�5/      ��  � ��$ 5q�6'   �� 50       � ��$	 200��6&   51      �� 54   
   " ��6   .�55      ��  � ��8   6+   �� 56       � ��
 �����6+   �� 57       � ��$ 16(�6)   �� 58       � ��$
 Bold"�6+   �� 59       � ��$ 0q�60   �� 5:       �@�Cursor$
 Hand��6'   �� 5;       � ��$ 5q�6'   �� 5<       � ��$ 45��6'   �� 5=       � ��$	 126��6&   5>      �� 5A   
   " 	�6   .�5B      ��  � ��$ 5,0,5,0q�6'   5C      5D      .��5F      � -   5        $��MainGrid��5F      $��
10,10,10,6q�6    � RowDefinitions 5H   
   �� 5I      $
��Auto �6   �� 5K      $
��Auto �6   �� 5M      $
��Auto �6   �� 5O      $
��Auto �6   �� 5Q      $
��Auto �6   �� 5S      $
��Auto �6   �� 5U      $
��Auto �6   �� 5W      $
��Auto �6   �� 5Y      $
��Auto �6   �� 5[      $
��Auto �6   �� 5]      $
��Auto �6   �� 5_      $
��Auto �6   �� 5a      $
��Auto �6   �� 5c      $��* �6   �� 5e      $
��Auto �6   	 � ColumnDefinitions	 5j   
   �� 5k      $
��Auto �6   �� 5l      $��* �6   .+�5p   
   �� $��Login��6   $��0��6    $��0��6-   �� 5q   
   -   5        
 ��Name$
 Textbox_login��5q      $��0��6)   $��1��66   �� 5t   
   $��Imię��6   $��1��6   $��0��6,   �� 5u   
   -   5        $
 Textbox_imie��5u      $��1��6(   $��1��65   6E   �� 5x   
   $��Nazwisko��6   $��2��6#   $��0��60   �� 5y   
   -   5        $
 Textbox_nazwisko��5y      $��2��6,   $��1��69   6I   �� 5|   
   $��Miejscowość��6   $��3��6&   $��0��63   �� 5}   
   -   5        $
 Textbox_miejscowość��5}      $��3��6/   $��1��6<   6L   �� 5�   
   $��Kod pocztowy��6   $��4��6'   $��0��64     5�   
   -   5          Name$ Textbox_kod_pocztowy��5�      $��4��6;   $��1��6H      Mask$ 00-000��6X      
PromptChar$ _��6f   $��1��6u   �� 5�   
   $��Ulica (opcjonalnie) ��6   $��5��6/   $��0��6<   �� 5�   
   -   5        $
 Textbox_ulica��5�      $��5��6)   $��1��66   �� 5�   
   $��Numer posesji��6   $��6��6(   $��0��65   �� 5�   
   -   5        $
 Textbox_numer_posesji��5�      $��6��61   $��1��6>   �� 5�   
   $��Numer lokalu (opcjonalne)��6   $��7��64   $��0��6A   �� 5�   
   -	   5        $
 Textbox_numer_lokalu��5�      $��7��60   $��1��6=   �� 5�   
   $��Pesel��6   $��8��6    $��0��6-   �� 5�   
   -
   5        $
 Textbox_pesel��5�      $��8��6)    �� 	MaxLength$ 11��66   $��1��6E   6U   �� 5�   
   $��Data urodzenia��6   $��9��6)   $��0��66     5�   
   -   5          Name$  Date_picker_data_urodzenia��5�      $��9��69   $��1��6F   $��Center=�6V   $��Center=�6s   �� 5�   
   $��Płeć��6   $��10��6   $��0��6-   �� 5�   
   $��10��6   $��1��6#    �� Orientation$ 
Horizontal=�63   $��Center=�6L   .+�5�      	� -   5         	�Name$ Radio_btn_kobieta��5�      $��Kobieta��65   	� 5�      -   5        $ Radio_btn_mężczyzna��5�      $��Mężczyzna��67   5�      �� 5�   
   $
��Mail��6   $��11��6   $��0��6-   �� 5�   
   -   5        $
 Textbox_mail��5�      $��11��6(   $��1��66   �� 5�   
   $��Numer telefonu��6   $��12��6)   $��0��67   �� 5�   
   -   5        $
 Textbox_numer_telefonu��5�      $ 9��62   $��12��6p   $��1��6~   �� 5�   
   $��13��6   $��0��6#   $��2��63   $ 
Horizontal=�5�      $��Center=�5�      .+�5�      �� -   5         ��Name$ buttonEdytuj��5�      $��Zapisz��6+   6<   �� 5�      -   5        $��Anuluj��5�      $	��140��5�      $��45��5�      ���#94A3B85�      5�      5�      5�      