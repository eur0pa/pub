#!/bin/bash
# Alice AGPF/AGIF router checker
# tells ya if you're onto target
#

echo
essid="${1#Alice-}"; bssid="${2//:/}";		echo " essid=$essid"'		# essid value'
bssid="${bssid^^}";				echo " bssid=$bssid"'	# wifi mac'
v0="55E63B89";					echo '   $v0='"$v0"'		# li 0x55E63B8, $v0'
v1="${bssid:5:7}";				echo '   $v1='"$v1"'		# mflo $v1 (bssid)'
v0=$(echo "obase=16;ibase=16; $v0 * $v1" | bc);	echo '   $v0='"$v0"'	# mult $v1, $v0'
hi="${v0:0:7}";					echo '   $hi='"$hi"'		# mfhi $v0 (^- this mult)'
let "v0=0x$hi >> 25";				echo '   $v0='"$v0"'		# srl $v0, 25 (^- this hiword)'
a3="5F5E100";					echo '   $a3='"$a3"'		# li 0x5F5E100, $a3'
let "t0=0x$a3 * $v0";				echo '   $t0='"$t0"'	# mult $t0, $v0, $a3 (multiply $a3 and $v0)'
let "a3=0x$v1 - $t0";				echo '   $a3='"$a3"'		# subu $a3, $v1, $t0 (subtract $t0 and $v1)'

[ $essid -eq $a3 ] && \
	echo -e "\n\tAlice-$essid is an AGPF/AGIF router\n" || \
	echo -e "\n\tAlice-$essid is not an AGPF/AGIF router\n"
