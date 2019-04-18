pandoc --output Readme.pdf -f gfm Readme.md --pdf-engine=lualatex --include-before-body=%LocalAppData%\Pandoc\raggedright.txt --highlight-style=tango --variable=papersize:letter --variable=geometry:margin=0.75in --variable=colorlinks:true --metadata=mainfont:LatinModernSans
pause
