   M S B A M L   `   `   `  ����S  OTrainScheduleApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:TrainScheduleApplicationTrainScheduleApplication  5   	   )    #TrainScheduleApplication.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:TrainScheduleApplication     5   	   -   5          � Title$   Train Schedule Application��5   	   $	��600��6,   $	��800��69    � Icon$ 	train.png��6E   .��5	      � .+�5   
   o� -   5         o�Name$
 menu��5      $��20��6   $��0,0,0,0q�6)   $	��Top=�6:   .@�5      m� $
��File��6   .@�5      m� -   5        $��Open Branch Schedule...��5      6=   �� 5      m� 5      -   5        $��_Exit��5       J� RenderTransformOrigin$ 0.489,0.576&�6*   6N   5      m� 5      $��Import��6   .@�5      m� -   5        $$��Import Stations from JSON File��5      6D   5      m� 5      $
��Help��6   .@�5      m� -   5        $��About��5      6*   5      5      �� 5      -   5         ��Name$ 
tabControl��5      $��0,20,0,0q�61   .@�5      �� $��All Stations��6   .��5       � ���	#FFE5E5E5 � ColumnDefinitions 5"      �� 5#   "   $	��25* �63   �� 5$   "   $��5* �63   �� 5%   "   $	��70* �63   .+�5(      �� -   5         ��Name$ gridSplitter��5(   +   $��1��6A   $��0q�6Q   $��5��6\   $��Center=�6f   ���	#FFDEB8B86�   �� 5)      -	   5         ��Name$ listBox_stations��5)   &   $��
10,35,0,10q�6@   5.      �� 5/      -
   5         ��Name$ label_sName��5/   $   $��Station Name��69   $
��Left=�6P   $��5,5,0,0q�6k   $	��Top=�6|   �� 52      -   5        $ lb_name��52   $   $
��Name��65   $��2��6D   $
��Left=�6T   $��	30,30,0,0q�6o   $	��Top=�6�   $	��132��6�   �� 53      -   5        	 ��Name$	 tb_name��53   &   $��2��67   $��23��6G   $��200,30,42,0q�6S   
 �� TextWrapping$

 Wrap=�6h   $	��Top=�6|   ��6�   �� *SelectedItem.Name+ �� ElementName$ listBox_stations���� 57      -   5        $ lb_sid��57   $   $��
StationId ��64   $��2��6I   $
��Left=�6Y   $��	30,60,0,0q�6t   $	��Top=�6�   �� 58      -   5        $	 tb_sid��58   &   $��2��66   $��23��6F   $��200,60,42,0q�6R   $

 Wrap=�6g   $	��Top=�6{   ��6�   �� *SelectedItem.Id+$ listBox_stations���� 5:      -   5        $ lb_location��5:   $   $��Location��69   $��2��6L   $
��Left=�6\   $��	30,90,0,0q�6w   $	��Top=�6�   $	��132��6�   �� 5<      -   5        $	 tb_location��5<   &   $��2��6;   $��35��6K   $��200,90,41,0q�6W   $

 Wrap=�6l   $	��Top=�6�   ��6�   �� *SelectedItem.Location+$ listBox_stations���� 5>      -   5        $ lb_farezoney��5>   $   $��FareZone��6:   $��2��6M   $
��Left=�6]   $��
30,130,0,0q�6x   $	��Top=�6�   $	��132��6�   �� 5?      -   5        $	 tb_farezone��5?   &   $��2��6;   $��23��6K   $��200,130,41,0q�6W   $

 Wrap=�6m   $	��Top=�6�   ��6�   �� *SelectedItem.FareZone+$ listBox_stations���� 5A      -   5        $ lb_mileagetopenn��5A   $   $��MileageToPenn��6>   $��2��6V   $
��Left=�6f   $��
30,160,0,0q�6�   $	��Top=�6�   $	��132��6�   �� 5B      -   5        $	 tb_mileagetopenn��5B   &   $��2��6@   $��23��6P   $��200,160,40,0q�6\   $

 Wrap=�6r   $	��Top=�6�   ��6�   �� *SelectedItem.MileageToPenn+$ listBox_stations���� 5D      -   5        $ lb_picfilename��5D   $   $��Picture��6<   $��2��6N   $
��Left=�6^   $��
30,200,0,0q�6y   $	��Top=�6�   $	��132��6�   �� 5E      -   5         ��Name$ image_Station��5E   $   $��2��6;   $	��152��6K   $��200,200,40,0q�6X   $	��Top=�6n   $	��309��6�   $
��Fill=�6�   $
��Left=�6�   5F      5G      �� 5J      $��Branch Schedule��6   .��5K      � ���	#FFE5E5E5 5M      �� 5N   "   $	��25* �63   �� 5O   "   $��5* �63   �� 5P   "   $	��70* �63   .+�5S      �� -   5        $ gridSplitter_��5S   +   $��1��6B   $��0q�6R   $��5��6]   $��Center=�6g   ���	#FFDEB8B86�   �� 5T      -   5        $ listBox_branchtrains��5T   &   $��10,100,0,10q�6D   5W      �� 5X      -   5        $ label��5X   $   $��	Branch ID��63   $
��Left=�6G   $��	10,20,0,0q�6b   $	��Top=�6u   $��86��6�   �� 5Y      -   5        $	 tb_BID��5Y   &   $
��Left=�66   $��23��6Q   $��	80,24,0,0q�6]   $

 Wrap=�6p   $	��Top=�6�   $��93��6�   $ 
0.624,1.13&�6�   .��6�    � ToolTip 5Z   "   _� 5[   &   .��5\   *   �� $)��#Id for the branch that was read in.��64   5]   '   .��5^   3   5`      �� 5a      -   5        $ 
label_Copy��5a   $   $��Branch Train ID's��68   $
��Left=�6T   $��	10,65,0,0q�6o   $	��Top=�6�   $��98��6�   �� 5b      -   5        $ label_Copy1��5b   $   $��Station Arrivals��69   $
��Left=�6T   $��3,34,0,0q�6o   $	��Top=�6�   $��98��6�   $��2��6�   �� 5d      -   5         ��Name$ listView_stationarrival��5d   '   $��2��6H   $��	2,104,0,0q�6X    �� View 5e   "   �� 5f   &   .P�5g   *   �� $��
Station ID��69    �� DisplayMemberBinding 6M   �� *	StationId+�� 5h   *   $
��Time��69    6G   �� *Time+5i   '   5k      5m      5n      5o      5s      5t      