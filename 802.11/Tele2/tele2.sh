#!/bin/bash

wmac="${1::8}" ; wmacc="${wmac//:/}" ; wmaccd="$((0x${wmacc}))"
mmac="${1:9}"  ; mmacc="${mmac//:/}" ; mmaccd="$((0x${mmacc}))"

while read -r line; do
        IFS=' ' read -r WMAC FROM TO SN1 BASE INCR NULL <<< "$line"
        if [[ $wmac == $WMAC ]]; then
                FROMC="${FROM//:/}" ; FROMCD="$((0x${FROMC}))"
                TOC="${TO//:/}"     ; TOCD="$((0x${TOC}))"
                if [ $mmaccd -ge $FROMCD ] && [ $mmaccd -le $TOCD ]; then
                        BASECD="$((0x${BASE}))"
                        DIFF=$((($mmaccd - $BASECD) / $INCR))
                        printf "%dY%07d\n" $SN1 $DIFF
                fi
        fi
done < tele2.txt
