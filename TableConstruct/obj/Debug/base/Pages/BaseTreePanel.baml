   M S B A M L   `   `   `  ����  TableConstruct"    TableConstruct.BaseTreePanel   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5         BaseTreeItem �� 
TargetType �� Property �� Value �� RelativeSource �� AncestorType  TreeViewItemFocusVisual �� 
TargetType � ColumnDefinitions � RowDefinitions	 d�Name
 �� 	ClickMode  ExpandCollapseToggleStyle d� 	IsChecked ��Name �� Padding J� SnapsToDevicePixels ��Name ��Name �� Triggers P� Property P� Value �� 
TargetName  ItemBackgroundSelected  ItemTextSelected ]� 
Conditions �� Property �� Value �� Triggers��%p  &	        7  7  �� 5+   
   " Q�6%   .�5,   
   ��  � ��
 ���� 6'   �� 5-   
    � �� 67   �� *HorizontalContentAlignment+ �� " ���� 5.   
    � �� 65   �� *VerticalContentAlignment+ �� " ���� 5/   
    � ��$ 1,0,0,0q�6$   �� 50   
    � ��# � ��6'   �� 51   
    �@�FocusVisualStyle8   6-   -   5        �� 53   
    � �� 54      �� 55      " Q�6"   .R�56      �  57      �� 58      $��19��6/   $
��Auto �6=   �� 59      $
��Auto �6/   �� 5:      $��* �6/    5<      �� 5=      $
��Auto �6,   �� 5>      .+�5@      d� $	 Expander��6'   $
 Press=�69   8��  6K   �� *
IsExpanded+ �� *TemplatedParent+6�   �� 5A      $ Bd��6!   #��z��6-   #��z��6Y   #��z��6�   $��1��6�   # z��6�    . ._�5B      �� $ PART_Header��6/   $��Header��6D   #��z��6[   # z 6�   5C      �� 5D      $ 	ItemsHost��6)   $��2��6<   $��1��6P   $��1��6`   5E       5F      P� 5G       �@Q�
IsExpanded .  . �5H      �� $ 	ItemsHost��6;    � y�$ 	Collapsed=�6R   5I      P� 5J       �@��HasItems .  . �5K      �� $ Expander��6;    � y�$ Hidden=�6Q   5L      P� 5M       �@Q�
IsSelected . . �5N   "   �� $ Bd��6)   # �  6O    � ���� 5O   "   # �  6?    � ���� 5P   "    � ��$
 Bold"�6?   5Q      ]� 5R       5S   "   �� 5T   &    �@Q�
IsSelected . 6F   �� 5U   &    �@Q�IsSelectionActive .  6M   ./�5W   "   �� $ Bd��6?   # � ��6O    � ���� 5X   "   # � ��6?    � ��5Y      P� 5Z       � {� .  . �5[      �� # � ��6;    � ��5\      5^      5`       5a   
   P� 5b       �@%�IsVirtualizing . . �5c      ��  � �� 5d      �� 5e      .R�5f      $� 5g      5i      5j      5l   
   5m      .��5n      � -   5         �Name$ _TreeViewPanel��5n      .+�5o   
   �� -   5         ��Name$ _ScrollViewer��5o      $
��auto=�6.   $
��Auto=�6S   .��5p      R� -   5         R�Name$ 	_TreeView��5p   $   $��1��6   #��[  5s       J� 	AllowDrop .  � ContextMenu 5{      �� 5|      .@�5}      m� -   5        $��删除��5}   #   6I   m� 5~      -   5        $��改名��5~   #   6I   m� 5      -   5        $��新建文件��5   #   6L   m� 5�      -   5        $��新建目录��5�   #   6N   5�      ��5�      �� 5�       �� ItemsSource 6/   ��  �� Path$ Children�.R�5�      �� $ listItemBorder��5�   !   $	��200��5�   !   #���  5�   !   ._�5�      ��   �� Orientation$  
Horizontal=�6)   $��10,2,2,2q�6B   .+�5�   "   �� $��!��5�   )   $��0,0,0,0q�62   $��12(�5�   )   
���  ��$��CharacterEllipsis=�5�   )   y�5�   ,   �� $ IsShowError�! �� Mode$! OneWay=�" �� ValidatesOnDataErrors" .     BooleanToVisibilityConverter# �� 	Converter## [ 5�   L   �� 5�   "   $��12(�5�   )   $��CharacterEllipsis=�5�   )   ��5�   ,   �� $
 Name�$! OneWay=�" .  5�   L   5�      5�      5�      5�      5�      5�      5�      