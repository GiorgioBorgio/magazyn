   M S B A M L   `   `   `  ����B  >Magazyn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:MagazynMagazyn  5          Magazyn.ForgottenUser   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      'localclr-namespace:Magazyn     5      ���#F2F4F7  � 	Resources  5
         	DarkGreen �� Color  LightBackground  TextMain  
BadgeGreen  RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius��%�  &	        &	 !     &	 @     &	 a     &	 �     �� 5   
   $ #134E4A��6,   �� 5   
   $ White��62   �� 5   
   $ #1E293B��6+   �� 5   
   $ #22C55E��6-   �� 5   
   " ��6+   .�5      ��  � �� �#94A3B86+   �� 5       � ��
 �����6+   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ 14(�6)   �� 5       � ��$ 0q�60   �� 5       � ��$
 10,5q�6(   �� 5       � ��$ 0,20,0,0q�6'   �� 5       � ��$	 140��6&   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   $ 8��6K   ._�5      �� $��Center=�6/   $��Center=�6L   5       5!      5#      5$      5(      .��5*      � .+�5,   
   �� #���  6   $��60��69   $	��Top=�6E   ._�5-      ]� $
��20,0q�6   $��False��6&   .+�5.      �� $,��&📋 Lista użytkowników zapomnianych��6   $��18(�5/      
�������$
��Bold"�51      $��Center=�52      53      54      �� 57   
   #��[ 6   $ 12��6?   $��40,80,40,20q�6Q    �� Padding$ 20q�6f   ._�58      �  � RowDefinitions 59      �� 5:      $
��Auto �6$   �� 5;      $��* �6$   .+�5?      ]� $��0��6   $��0,0,0,10q�6)   $	��Top=�6;   $
��Left=�6S   .+�5@      �� ���#E2E8F0���#CBD5E1$��1q�6H   $ 6��5A      $��38��6*   $	��270��66   ._�5B      � $
��10,0q�6   .+�5C      �� $
��🔍��6(   $��16(�62   $��Center=�6@   $��0,0,6,0q�5D   $   ���#334155$
��Left=�6J   �� 5E      -   5        	 ��Name$	 PlaceholderText��5E   (   $��Wyszukaj��6A   $��13(�6Q   
�������$��24,0,0,0q�5F   $   $��Center=�66   
 J� IsHitTestVisible$
 False��6Q   $y�Visible=�6j   �� 5G      -   5        $��TextBoxSearch��5G   &   $	��200��6;   $��0q�6G   
������ ���#1E293B$��13(�5H   7   $��Center=�6E   $��24,0,0,0q�5I   "   $�� ��6\   5J      5K      5L      &   System.Windows.Controls.DataGrid  5O      -   5          Name$ ForgottenDataGrid��5O      $��1��66      AutoGenerateColumns$ False��6C      HeadersVisibility$ Column=�6_      
IsReadOnly$
 True��6z      CanUserAddRows$ False��5P         CanUserReorderColumns$ False��6.      CanUserSortColumns$ False��6L      	RowHeight$ 60Q�5Q         RowHeaderWidth$ 0Q�6&      GridLinesVisibility$
 None=�69   $��0q�6T      AlternatingRowBackground
 �����   RowBackground
 �����   ColumnHeaderHeight$ 40Q�5R   N      EnableRowVirtualization$ False��5S         SelectionMode$ Single=�67      SelectionUnit$ FullRow=�6N   
������    Columns 5U      0  *System.Windows.Controls.DataGridTextColumn  5V      ,  &System.Windows.Controls.DataGridColumn   Header$ Identyfikator��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6D   �� *Id+6[     5W      $ Imię��6-   $ * 6Y    6;   �� *	FirstName+6Y     5X      $ Nazwisko��6-   $ * 6\    6?   �� *
LastName+6\     5Y      $ Data zapomnienia��6-   $ * 6�    6G   �� *ForgottenDate+ �� StringFormat$ dd.MM.yyyy HH:mm��6�     5Z      $ Zapomniany przez��6-   $ * 6i    6G   �� *ForgottenById+6i   5\      5]      5^      �� 5a   
   -   5        $��↩ Powrót��5a      $	��120��5b      $��40��5c      $��Right=�5d      $	��Top=�5e      $��	0,10,10,0q�5f      #��[ 5g        K� ZIndex$	  100��5i      5j      5k      