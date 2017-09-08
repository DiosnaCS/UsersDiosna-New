    // globální konstanty

    // globální proměnné
    var fontFamily = "Tahoma",
        d = new Date();
    
    // layout
    var cOff = 0.5,
        posX = cOff,
        posY = cOff,                
        fieldBrake = 5,
        fieldTopAdjust = 50,         
        fieldBottomAdjust = 50,        
        fieldLeftAdjust = 40,
        fieldRightAdjust = 20,
        fieldValWidth = 315,
        fieldBreakVals = 15,
        poleBegtimeOdsazY = 30,
        titleBarHeight = 20,
        canvasOffsetLeft,
        canvasOffsetTop,
        SetPosMax = new Array(),   // y position for input max limit
        SetPosMin = new Array(),   // y position for input min limit
        colors = ["#FF0000","#FFFF00","#00FF00","#00FFFF","#FF00FF","#FF7F28","#FFFFFF","#FF0000","#FFFF00","#00FF00","#00FFFF","#FF00FF","#FF7F28","#FFFFFF","#FF0000","#FFFF00","#00FF00","#00FFFF","#FF00FF","#FF7F28","#FFFFFF","#FF0000","#FFFF00","#00FF00","#00FFFF","#FF00FF","#FF7F28","#FFFFFF","#FF0000","#FFFF00","#00FF00","#00FFFF"],        
        firstInit,
        iWidth,
        iHeight,
        chartWidth,
        chartHeight;     
        
    // pole konstant - kroky mřížky,délka osy
    var stepGridY = [1,2,5,10,20,50,100,200,500,1000,2000,5000,10000],
        stepGridTime =[5,10,20,30,60,60,120,240,1440,2880],
        timeAxisLength = [0.5,1,2,4,8,12,24,48,168,336],
        viewLength = [720,1080,1440], 

        monthsEN = ["January","February","March","April","May","June","July","August","September","October","November","December"],
        monthsCZ = ["Leden","Únor","Březen","Duben","Květen","Červen","Červenec","Srpen","Září","Říjen","Listopad","Prosinec"],
        monthsDE = ["Januar","Februar","März","April","Mai","Juni","Juli","August","September","Oktober","November","Dezember"],
        monthsPL = ["Styczeń","Luty","Marzec","Kwiecień","Maj","Czerwiec","Lipiec","Sierpień","Wrzesień","Październik","Listopad","Grudzień"],
        monthsRU = ["январь","февраль","март","апрель","май","июнь","июль","август","сентябрь","октябрь","ноябрь","декабрь"],

        weekday = new Array();

        daysEN0 = ["Sun","Mon","Tue","Wed","Thu","Fri","Sat"],
        daysCZ0 = ["Ne","Po","Út","St","Čt","Pá","So"],
        daysDE0 = ["So","Mo","Di","Mi","Do","Fr","Sa"],
        daysPL0 = ["Nie","Po","Wt","Śro","Czw","Pią","So"],
        daysRU0 = ["вс","пн","вт","ср","чт","пт","cб"],        

        daysEN1 = ["Mon","Tue","Wed","Thu","Fri","Sat","Sun"],
        daysCZ1 = ["Po","Út","St","Čt","Pá","So","Ne"],
        daysDE1 = ["Mo","Di","Mi","Do","Fr","Sa","So"],
        daysPL1 = ["Po","Wt","Śro","Czw","Pią","So","Nie"],
        daysRU1 = ["пн","вт","ср","чт","пт","cб","вс"];

    // limity
    var MAX_VIEWS = 30,
        MAX_FIELDS = 9,
        MAX_SIGNALS = 16,
        MAX_MULTITEXTS = 39,
        MAX_HORIZ_LINE = 5,
        MAX_CLICKMAPS = 50;

    // menu
    var factory,        
        utcBias = 0,
        lang = 0,        
        timeAxisIdx = 7,
        beginTime,
        agregBeginTime = 0,
        lastGroup = 0,        
        markerTime;

    // fonty
    var fontTitle = 'bold 12px MS Sans Serif',
        fontLegend = 'normal 11px MS Sans Serif',
        fontVals = 'bold 12px MS Sans Serif',
        fontAxis = 'normal 10px MS Sans Serif';

    // barvy    
    var colorWarning = '#FF0000',   
        colorView = '#F0F0F0',       
        colorBackFields = '#000000',
        colorGrid = '#404040',
        colorTitle = '#000000',
        colorBackTime = '#000000',
        colorTime = '#FFFFFF',
        colorFrames = '#000000';
        BLACK = '#000000';
        WHITE = '#FFFFFF';

    var chartMaps;

        // config
        multitexts = {0: {"name": "", "values": []},
                      1: {"name": "", "values": []},
                      2: {"name": "", "values": []},
                      3: {"name": "", "values": []},
                      4: {"name": "", "values": []},
                      5: {"name": "", "values": []},
                      6: {"name": "", "values": []},
                      7: {"name": "", "values": []},
                      8: {"name": "", "values": []},
                      9: {"name": "", "values": []},
                      10: {"name": "", "values": []},
                      11: {"name": "", "values": []},
                      12: {"name": "", "values": []},
                      13: {"name": "", "values": []},
                      14: {"name": "", "values": []},
                      15: {"name": "", "values": []},
                      16: {"name": "", "values": []},
                      17: {"name": "", "values": []},
                      18: {"name": "", "values": []},
                      19: {"name": "", "values": []},
                      20: {"name": "", "values": []},
                      21: {"name": "", "values": []},
                      22: {"name": "", "values": []},
                      23: {"name": "", "values": []},    
                      24: {"name": "", "values": []},
                      25: {"name": "", "values": []},                                            
                      26: {"name": "", "values": []},
                      27: {"name": "", "values": []},                                            
                      28: {"name": "", "values": []},
                      29: {"name": "", "values": []},
                      30: {"name": "", "values": []},
                      31: {"name": "", "values": []},
                      32: {"name": "", "values": []},
                      33: {"name": "", "values": []},    
                      34: {"name": "", "values": []},
                      35: {"name": "", "values": []},                                            
                      36: {"name": "", "values": []},
                      37: {"name": "", "values": []},                                            
                      38: {"name": "", "values": []},
                      39: {"name": "", "values": []}
                     };
                     
          //nadefinování values
          for (var m=0;m<MAX_MULTITEXTS;m++) {
            for (var v=0;v<100;v++) {
              multitexts[m]["values"].push({"idx" : -99, "text": ""});
            };
          };
          
          var eventTmp = [{ "BatchNo": 0, "Fin": 1 }, { "BatchNo": 0, "Fin": 1 }, { "BatchNo": 0, "Fin": 1 }];

    /* mask description
       mask: "Src: $@Variant1#dest$,Dest: $@Destination#dest$@Actual#0.001,1,u:kg$/ $@Need#0.001,1,u:kg"
       $ ... odděluje jednotlive prvky
       @ ... uvozuje typ column
       # ... specifikuje detail - multitext (následuje písmeno)         / #dest
                                - koef, dec. places (následuje číslice) / #0.001,1,u:kg (coeficient *0.001, 1 decmal place, units kg)
       u .. uvozuje jednotky, za dvojtečkou je jejich id, pokud nenajde v seznamu units, pouzije se jednotka za "u:"                            
                                
    */
    var eventsConfig = {

          "recordTypes": [
          
            {"id": 10, "label": "recipe start", "mask": "@Destination#dest$/$@Need#0.001,1,kg$/$@Variant3#1/3600,1,hrs"},
            {"id": 14, "label": "recipe end", "mask": "@Destination#dest$/$@Actual#0.001,1,kg$/$@TimeEnd#1/3600,1,hrs$/$, BatchNo:$@BatchNo"},

            {"id": 20, "label": "motherdough", "mask": "Source: @Variant1#dest,Dest.: $@Destination#dest$@Actual#0.001,1,kg$/$@Need#0.001,1,kg"},
            {"id": 21, "label": "flour", "mask": "Source: $@Variant1#dest$,Dest.: $@Destination#dest$@Actual#0.001,1,kg$/$@Need#0.001,1,kg"},
            {"id": 22, "label": "water", "mask": "Source: $@Variant1#dest$,Dest.: $@Destination#dest$@Actual#0.001,1,kg$/$@Need#0.001,1,kg"},
            {"id": 23, "label": "old bread", "mask": "Source: $@Variant1#dest$,Dest.: $@Destination#dest$@Actual#0.001,1,kg$/$@Need#0.001,1,kg"},
            {"id": 24, "label": "yeast", "mask": "Source: $@Variant1#dest$,Dest.: $@Destination#dest$@Actual#0.001,1,kg$/$@Need#0.001,1,kg"},
            {"id": 25, "label": "component", "mask": "Source: $@Variant1#dest$,Dest.: $@Destination#dest$@Actual#0.001,1,kg$/$@Need#0.001,1,kg"},
            {"id": 28, "label": "motherdough", "mask": "Source: $@Variant1#dest$,Dest.: $@Destination#dest$@Actual#0.001,1,kg$/$@Need#0.001,1,kg"},
            {"id": 29, "label": "refil flour", "mask": "Source: $@Variant1#dest$,Dest.: $@Destination#dest$@Actual#0.001,1,kg$/$@Need#0.001,1,kg"},

            {"id": 31, "label": "filling mixture", "mask": "Src: $@Variant1#dest$,Dest: $@Destination#dest$@Actual#0.001,1,kg$/$@Need#0.001,1,kg"},
            {"id": 32, "label": "soaking (no mix)", "mask": "Tank: $@Variant1#dest$,Duration: $@Actual#0.01666666667,1,min$/$@Need#0.01666666667,1,min"},
            {"id": 33, "label": "mixing", "mask": "Tank: $@Destination#dest$,Duration: $@Actual#0.01666666667,1,min$/$@Need#0.01666666667,1,min"},
            {"id": 35, "label": "fermentation", "mask": "Tank: $@Destination#dest$,Duration: $@Actual#1/3600,1,hrs$/$@Need#1/3600,1,hrs$,Temp: $@Variant1#0.1,1,°C"},
            {"id": 36, "label": "cooling", "mask": "Tank: $@Destination#dest$,Duration: $@Actual#1/3600,1,hrs$/$@Need#1/3600,1,hrs$,Temp: $@Variant2#0.1,1,°C$/$@Variant1#0.1,1,°C"},
            {"id": 37, "label": "storing", "mask": "Tank: $@Destination#dest$,Duration: $@Actual#1/3600,1,hrs$/$@Need#1/3600,1,hrs"},
            {"id": 39, "label": "status info", "mask": "Tank: $@Destination#dest$,Mass: $@Actual#0.001,1,kg$,Temp: $@Variant1#0.1,1,°C$pH: $@Variant2#0.1,1,-"},

            {"id": 40, "label": "repumping", "mask": "Source: $@Variant1#dest$,Dest: $@Destination#dest$@Actual#0.001,1,kg$/$@Need#0.001,1,kg"},
            {"id": 44, "label": "dosing out", "mask": "Source: $@Variant1#dest$,Dest: $@Destination#dest$@Actual#0.001,1,kg$/$@Need#0.001,1,kg"},
            {"id": 45, "label": "pigging", "mask": "Dest.: $@Destination#dest"},
            {"id": 46, "label": "fermenter cleaning", "mask": "Dest.: $@Destination#dest"},
            {"id": 47, "label": "pipework cleaning", "mask": "Pipe: $@Destination#dest"}
          ],
          
          "units": {
            "langs": ["EN","CZ","DE","PL","RU"],
            "g": ["g","g","g","g","g"], 
            "kg": ["kg","kg","kg","kg","kg"],
            "s": ["s","s","s","s","s"], 
            "min": ["min","min","min","min","min"],
            "hrs": ["hrs","hod","Std","Godz","???"],
            "kPa": ["kPa","kPa","kPa","kPa","kPa"],  
            "Bar": ["Bar","Bar","Bar","Bar","Bar"]
          },

          "dest": ["","SF1","SF2","SF3","SF4","SF5","SF6","SF7","SF8","SF9","SF10","SF11","SF12","SF13","SF14","SF15","SF16","SF17","SF18","SF19","",
          "MF1","MF2","MF3","","","","","","","","PF1","PF2","PF3","PF4","","","","","","",
          "Line 1","Line 2","Line 3","Line 4","Line 5","Line 6","Line 7","Line 8","Line 9","DS 1","DS 2","DS 3","DS 4","","","","","",
          "gully","","","","","","","",""

          /*
           "dest": ["","","","","","","","","","","","MF1","MF2","MF3","","","","","","","","PF1","PF2","PF3","PF4","","","","","","",
           "SF1","SF2","SF3","SF4","SF5","SF6","SF7","SF8","SF9","SF10","SF11","SF12","SF13","SF14","SF15","SF16","SF17","SF18","SF19",
           "Line 1","Line 2","Line 3","Line 4","Line 5","Line 6","Line 7","Line 8","Line 9","DS 1","DS 2","DS 3","DS 4","","","","","",
           "gully","","","","","","","",""
          */
          ]
        };

    /*
       duration se dopocte TimeEnd - TimeStart
       BatchNo bude sloužit pro onClick - odskok na příslušný Batch v reportech
    */
    var eventData = {          
      "events": []
    };
   
    // zatím staticky později příjde ze serveru
    var alarmsConfig = {
          "EN": [
            "alarmen_0",
            "alarmen_1",
            "alarmen_2",
            "alarmen_3",
            "alarmen_4",
            "alarmen_5",
            "alarmen_6",
            "alarmen_7",
            "alarmen_8",
            "alarmen_9",
            "alarmen_10",
            "alarmen_11",
            "alarmen_12",
            "alarmen_13",
            "alarmen_14",
            "alarmen_15",
            "alarmen_16",
            "alarmen_17",
            "alarmen_18"
          ],
          "CZ": [
            "alarmcz_0",
            "alarmcz_1",
            "alarmcz_2",
            "alarmcz_3",
            "alarmcz_4",
            "alarmcz_5",
            "alarmcz_6",
            "alarmcz_7",
            "alarmcz_8",
            "alarmcz_9",
            "alarmcz_10",
            "alarmcz_11",
            "alarmcz_12",
            "alarmcz_13",
            "alarmcz_14",
            "alarmcz_15",
            "alarmcz_16",
            "alarmcz_17",
            "alarmcz_18"
          ],
          "DE": [
            "alarmde_0",
            "alarmde_1",
            "alarmde_2",
            "alarmde_3",
            "alarmde_4",
            "alarmde_5",
            "alarmde_6",
            "alarmde_7",
            "alarmde_8",
            "alarmde_9",
            "alarmde_10",
            "alarmde_11",
            "alarmde_12",
            "alarmde_13",
            "alarmde_14",
            "alarmde_15",
            "alarmde_16",
            "alarmde_17",
            "alarmde_18" 
          ]
        };
    
    var alarmData = {      
          "alarms": [
           {"id":10, "riseTime": 557305199, "expTime": 557345199},
           {"id":11, "riseTime": 557305199, "expTime": 557345199},
           {"id":12, "riseTime": 557305199, "expTime": 557345199},
           {"id":13, "riseTime": 557308799, "expTime": 557355199},
           {"id":14, "riseTime": 557305199, "expTime": 557345199},
           {"id":15, "riseTime": 557305199, "expTime": 0}
          ]
        };
                                   
    var titles = new Array();

    var currView;
    var currLang;
    var initial = true;
    var defConfig;    
    var config;
    var data;

    // operace myši a klávesnice
    var canX,
        canY,
        zoomCanX,
        lastCanX,
        dragCanX,
        mouseIsDown = false,
        shiftOn = false,
        zoom = false,
        setting = false,
        reset = false;
        
    // event listener
    document.addEventListener("DOMContentLoaded",init,false);

    // event window resize
    var resTime; 
    var isResize = false;
    var resDelta = 200;    

    $(window).resize(function() {
        resTime = new Date();
        if (isResize == false) {
           isResize = true;
           setTimeout(resizeEnd(), resDelta);
        };
    });

    function resizeEnd() {
      if (new Date() - resTime < resDelta) {
          setTimeout(resizeEnd, resDelta);
          isResize = true;
      } else {
          init();
      };
    };

    // inicializace
    function init() {

      // přizpůsobení ploše
      if (bMenuIsHide) {
        iWidth = windowWidth() - 25;       
      }
      else {
        iWidth = windowWidth() - $('.sidenav').width() - 25;
      };

      if  (firstInit) {
        iHeight = windowHeight() - $('.navbar').height() - $('#top_menu').height() - $('.message').height() - $('footer').height() - 10;
      }
      else {
        iHeight = windowHeight() - $('.navbar').height();
      };
      
      firstInit = false; 

      chartWidth = iWidth-fieldLeftAdjust-fieldBreakVals-fieldValWidth-fieldRightAdjust;
      chartHeight = iHeight-fieldTopAdjust-fieldBottomAdjust;

      $('#top_menu').css('width',iWidth);
      $('#top_menu').css('position','absolute');  
      $('#top_menu').css('top',$('.navbar').height()+$('.message').height());
      $('#top_menu').css('left',$('.sidenav').width()+20);

      $('#calendar').css('left',$('.sidenav').width()+367);

      if (bMenuIsHide) {
        $('#top_menu').css('left',10);
        $('#graph_content').css('left',10);
      }
      else {
        $('#top_menu').css('left',$('.sidenav').width()+20);
        $('#graph_content').css('left',$('.sidenav').width()+20);
      };
      
      $('#graph_content').css('top',$('.navbar').height()+$('.message').height()+$('#top_menu').height()+6);

      $('#active_canvas').attr('width',iWidth+1);
      $('#active_canvas').attr('height',iHeight+1);      

      $('#front_canvas').attr('width',iWidth+1);
      $('#front_canvas').attr('height',iHeight+1);

      $('#signal_canvas').attr('width',iWidth+1);
      $('#signal_canvas').attr('height',iHeight+1);

      $('#back_canvas').attr('width',iWidth+1);
      $('#back_canvas').attr('height',iHeight+1);     

      $('hr').css('position','absolute');
      $('hr').css('top',$('.navbar').height()+$('.message').height()+$('#top_menu').height()+iHeight+14);
      
      $('footer').css('position','absolute');
      $('footer').css('top',$('.navbar').height()+$('.message').height()+$('#top_menu').height()+iHeight+15);      
      
      // index nula vyhrazen pro oblast grafu
      // 1 - 50 pro viditelnost signálu
      // parametr offset je pro použití relativního odměřování >0<
      chartDef = {0: {"id": ["chart"], "coords": [posX+fieldLeftAdjust,posY+fieldTopAdjust,posX+fieldLeftAdjust+chartWidth,posY+fieldTopAdjust+chartHeight], "offset": 0, "times": [], "values": []},
                  1: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  2: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},                    
                  3: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  4: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  5: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  6: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  7: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  8: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  9: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  10: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  11: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  12: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  13: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  14: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  15: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  16: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  17: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  18: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  19: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  20: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  21: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  22: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  23: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  24: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  25: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  26: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  27: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  28: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  29: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  30: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  31: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  32: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  33: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  34: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  35: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  36: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  37: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  38: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  39: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  40: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  41: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  42: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  43: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  44: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  45: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  46: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  47: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  48: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  49: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                  //vyhrazeno pro eventy
                  50: {"id": [""], "coords": [], "visibility": ["true"], "offset": 0, "times": [], "values": []},
                 };

      // aktivní canvas - pro operace myši - kreslení kurzoru
      activeCanvasElement = $("#active_canvas");
      activeCanvas = activeCanvasElement[0].getContext('2d');      
      $('#activeCanvas').attr('width',iWidth);
      $('#activeCanvas').attr('height',iHeight);            
      activeCanvasElement[0].addEventListener("mousedown",mouseDown,false);
      activeCanvasElement[0].addEventListener("mousemove",mouseMove,false);
      activeCanvasElement[0].addEventListener("mouseup",mouseUp,false);      
      window.addEventListener("keydown",keyDown,false )
      canvasOffsetLeft = Math.round(activeCanvasElement.offset().left);
      canvasOffsetTop = Math.round(activeCanvasElement.offset().top);

      // popředí view
      frontCanvasElement = $("#front_canvas");
      frontCanvas = frontCanvasElement[0].getContext('2d');
      $('#frontCanvas').attr('width',iWidth);
      $('#frontCanvas').attr('height',iHeight);

      // signály - canvas má rozměr a umístění dle oblasti grafu
      signalCanvasElement = $("#signal_canvas");
      signalCanvas = signalCanvasElement[0].getContext('2d');
      $('#signalCanvas').attr('width',chartWidth);
      $('#signalCanvas').attr('height',chartHeight);      
      $('#signalCanvas').attr('top',posY+fieldTopAdjust);
      $('#signalCanvas').attr('left',posX+fieldLeftAdjust);  

      // pozadí grafu - mřížka
      backCanvasElement = $("#back_canvas");
      backCanvas = backCanvasElement[0].getContext('2d');
      $('#backCanvas').attr('width',chartWidth);
      $('#backCanvas').attr('height',chartHeight);
      $('#backCanvas').css('cursor','pointer');

      var timeSec = getCurrentTime();
      var mod = timeSec%(stepGridTime[timeAxisIdx]*60);

      beginTime = timeSec - mod - (timeAxisLength[timeAxisIdx]*3600 - (stepGridTime[timeAxisIdx]*60)); // + stepGridTime[timeAxisIdx]*60;

      view = lastGroup;                
      
      if (!isResize) {      
        getConfig();                
      }
      else {
        setupLayout();
        isResize = false;
      };
      
      $("#settingsWin").css('left', ($(".sidenav").width()+fieldLeftAdjust+21));      

      shiftLength(timeAxisIdx);

    };
    
    function windowWidth() {
      return $(window).width();
    };

    function windowHeight() {
      return $(window).height()-50;
    };    

    // operace klávesnice a myši

    function getClickStep () {    
      switch (timeAxisIdx) { 
      case 0:
        return 5;
        break;
      case 1:
        return 5;
        break;
      case 2:
        return 10;
        break;
      case 3:
        return 20;
        break;
      case 4:
        return 30;
        break;
      case 5:
        return 60;
        break;
      case 6:
        return 120;
        break;
      case 7:
        return 240;
        break;
      case 8:
        return 600;
        break;
      case 9:
        return 1800;
        break;
      };
    };
    
    function keyDown(event) {
    var clickStep = 5;

      // posun vlevo
      if (event.keyCode == 37) {
        if ((markerTime - clickStep) >= beginTime) {
          clickStep = getClickStep();
          markerTime = markerTime - (markerTime%clickStep) - clickStep;
          drawCursor(markerTime);          
        }
        else {
          backShift();
        };
      };
      
      // posun vpravo
      if (event.keyCode == 39) {
        if ((markerTime + clickStep) <= (beginTime + timeAxisLength[timeAxisIdx]*3600)) {
          clickStep = getClickStep();
          markerTime = markerTime - (markerTime%clickStep) + clickStep;
          drawCursor(markerTime);         
        }
        else {
          fwdShift();
        };
      };
    };

    function mouseDown(event) {
    var url_e;
    var win;

    mouseIsDown = true;
    
      if (bMenuIsHide) {
        canvasOffsetLeft = activeCanvasElement.offset().left;
      };                 
      
      canX = (event.x-canvasOffsetLeft)+0.5;      
      canY = (event.y-canvasOffsetTop)+0.5;
      
      // oblast grafu
      if ((canX > chartDef[0]["coords"][0]) && (canX < chartDef[0]["coords"][2]) && (canY > chartDef[0]["coords"][1]) && (canY < chartDef[0]["coords"][3])) {
        if (zoom == false) {
          drawCursor(canX);
        }
        else { // mód zoom
          zoomCanX = canX;          
        };
      };

      for (var m=1;m<MAX_CLICKMAPS;m++) {

        if ((canX  > chartDef[m]["coords"][0]) && (canX < chartDef[m]["coords"][2]) && (canY > chartDef[m]["coords"][1]) && (canY < chartDef[m]["coords"][3])) {
          if (chartDef[m]["visibility"] == "true") {
            chartDef[m]["visibility"] = "false";
          }
          else {
            chartDef[m]["visibility"] = "true";
          };
            
          view = $('#group').val();
          lang = getLang();          
          redrawChart(view,beginTime,markerTime,timeAxisIdx);
          // na obrazovce s fieldem binaries mizeli vsechny hodnoty...
          backShift();
          fwdShift();
          //
          redrawTimeAxis(view,lang);
          redrawLegend(view,lang);                              
          redrawValues(view,markerTime,lang);
        };
      };
    };

    function mouseUp() {
    
    var zoomLength = 0,
        zoomStartTime,
        zoomEndTime;
        
      if (bMenuIsHide) {
        canvasOffsetLeft = activeCanvasElement.offset().left;
      };
      
      canX = (event.x-canvasOffsetLeft)+0.5;
      canY = (event.y-canvasOffsetTop)+0.5;

      mouseIsDown = false;

      // oblast grafu - souřadnice X
      if ((canX > chartDef[0]["coords"][0]) && (canX < chartDef[0]["coords"][2])) {

        // operace ZOOM
        if (zoom == true) {
          drawCursor(canX);
          
          // výběr v pixelech
          zoomLength = Math.abs(canX - zoomCanX);

          // jakému času odpovídá 1px
          koefFieldWidth = timeAxisLength[timeAxisIdx]*3600/(chartWidth);          
          if (zoomCanX <= canX) {
            zoomStartTime = beginTime + Math.round((zoomCanX-fieldLeftAdjust)*koefFieldWidth);          
            zoomEndTime = beginTime + Math.round((canX-fieldLeftAdjust)*koefFieldWidth);
          }
          else {
            zoomEndTime = beginTime + Math.round((zoomCanX-fieldLeftAdjust)*koefFieldWidth);          
            zoomStartTime = beginTime + Math.round((canX-fieldLeftAdjust)*koefFieldWidth);
          };
          
          for (var i=0;i<=timeAxisLength.length;i++) {
          
            if ((zoomLength*koefFieldWidth/3600) < timeAxisLength[i]) {              
              timeAxisIdx = i;
              if (((zoomEndTime - zoomStartTime)/3600) > (timeAxisLength[i])) {
                timeAxisIdx = timeAxisIdx++;
              };
              
              // přepočet počátku grafu dle kroku nové mřížky           
              beginTime = zoomStartTime - (zoomStartTime%(stepGridTime[timeAxisIdx]*60));
              getData(view,beginTime,timeAxisLength[timeAxisIdx]*3600);
              getEventsHeader(beginTime,timeAxisLength[timeAxisIdx]*3600);

              // marker umístíme na konec zoom oblasti
              markerTime = beginTime + timeAxisLength[timeAxisIdx]*3600;

              view = $('#group').val();
              lang = getLang();
              shiftLength(timeAxisIdx);
              redrawChart(view,beginTime,markerTime,timeAxisIdx);
              redrawTimeAxis(view,lang);
              redrawValues(view,markerTime,lang);
              redrawBeginTime(beginTime,lang);
              redrawMarkerTime(markerTime,lang);
              break;          
            };
          };
          zoom = false;
          zoomSignal("ZOOM");
        };

        // events
      var url_curr = new URL(document.URL);
      
      
      // zacátek události
      if ((canY > chartDef[50]["coords"][1]) && (canY < chartDef[50]["coords"][3])) {

        for (e = 0; e < eventData.events.length; e++) {
          if ((Math.abs(markerTime-eventData.events[e].TimeStart) <= 300) && (Math.abs(eventData.events[e].TimeEnd-markerTime) <= 300)) {

            plc = url_curr.searchParams.get("plc");

            var reportWin = window.open("http://users2.diosna.cz/Report?plc=" + plc, '_blank');
            
            var url_e = "http://users2.diosna.cz/Report/Detail/" + eventData.events[e].BatchNo;
            window.open(url_e, '_blank');

            //reportWin.close();
          };
        };
      };

      };      
    };

    function mouseMove() {
      if (bMenuIsHide) {
        canvasOffsetLeft = activeCanvasElement.offset().left;
      };
      
      canX = (event.x-canvasOffsetLeft)+0.5;
      canY = (event.y-canvasOffsetTop)+0.5;
            
      // oblast grafu - souřadnice X
      if ((canX > chartDef[0]["coords"][0]) && (canX < chartDef[0]["coords"][2])) {
      
        if ((mouseIsDown) && (!zoom)) {
          koefFieldWidth = timeAxisLength[timeAxisIdx]*3600/(chartWidth);
          markerTime = beginTime + Math.round((canX-fieldLeftAdjust)*koefFieldWidth);
          drawCursor(canX);
          redrawMarkerTime(markerTime,lang);
          lastCanX = canX;
        };
        
        if ((mouseIsDown) && (zoom)) {
          drawCursor(zoomCanX);
          drawCursor(canX);
        };        
      };
      
      for (var m=1;m<MAX_CLICKMAPS;m++) {        
        if ((canX  > chartDef[m]["coords"][0]) && (canX < chartDef[m]["coords"][2]) && (canY > chartDef[m]["coords"][1]) && (canY < chartDef[m]["coords"][3])) {          
          document.getElementById("graph").style.cursor = "pointer";
        }
        else {
          document.getElementById("graph").style.cursor = "auto";
        };
      };  
    };

    // vykreslení kurzoru 
    function drawCursor(x) {    
    var markerX,
        timeX;
    
      view = $('#group').val();
      lang = getLang();

      //jakému času odpovídá 1px
      koefFieldWidth = timeAxisLength[timeAxisIdx]*3600/(chartWidth);
         
      // marker time
      if (x > 100000) {
        
        markerTime = x;
        timeX = (markerTime - beginTime);        
        if (timeX < (timeAxisLength[timeAxisIdx]*3600)) {

        eraseActiveCanvas(0);
        
        redrawMarkerTime(markerTime,lang);

        markerX = Math.round(timeX/koefFieldWidth);

        redrawValues(view,markerTime,lang);
            
        activeCanvas.strokeStyle = WHITE;

        activeCanvas.beginPath();
          activeCanvas.moveTo(posX+fieldLeftAdjust+markerX,posY+fieldTopAdjust);
          activeCanvas.lineTo(posX+fieldLeftAdjust+markerX,posY+fieldTopAdjust+chartHeight);
          activeCanvas.stroke();
        activeCanvas.closePath();        
        
        }; 
      }
      
      // pozice x
      else {
          if ((mouseIsDown) && (zoom)) {                        
            eraseActiveCanvas(x);
          };          
          
          if ((mouseIsDown) && !(zoom)) {
            eraseActiveCanvas(0);

            // jakému času odpovídá 1px            
            markerTime = beginTime + Math.round((canX-fieldLeftAdjust)*koefFieldWidth);            
            redrawMarkerTime(markerTime,lang);
            redrawValues(view,markerTime,lang);             
          };
          
          if (!(mouseIsDown) && !(zoom)) {                                                
            eraseActiveCanvas(x-1);
            eraseActiveCanvas(x-2);
            eraseActiveCanvas(x-3);
          };
          
          activeCanvas.strokeStyle = WHITE;
          
          activeCanvas.beginPath();
            activeCanvas.moveTo(x,posY+fieldTopAdjust);
            activeCanvas.lineTo(x,posY+fieldTopAdjust+chartHeight);
            activeCanvas.stroke();
          activeCanvas.closePath();
      };
    };
    
    function eraseActiveCanvas(s) {        
      x = (s == 0) ? posX : s;
      width = (s == 0) ? iWidth : canX-zoomCanX;
      activeCanvas.clearRect(x,posY,width,iHeight);
    };

    // globalni funkce
    function addZero(i) {    
      i = i < 10 ? "0" + String(i) : i;      
      return i;
    };
       
    function getFormatedDateTime(inPktime,lang) {
    var pktime = new Pktime();
    var dateTimeString = "";

      switch (lang) {
      case "EN":
        weekday = daysEN0;
        break;
      case "CZ":
        weekday = daysCZ0;
        break;
      case "DE":
        weekday = daysDE0;
        break;
      case "PL":
        weekday = daysPL0;
        break;
      case "RU":
        weekday = daysRU0;
        break;    
      };
    
      pktime.setValue(inPktime);      
      var dateTime = pktime.getDate();
      
      var day = weekday[dateTime.getDay()];
      var dd = addZero(dateTime.getDate());
      var MM = addZero(1 + dateTime.getMonth());
      var yyyy = dateTime.getFullYear();
      var hh = addZero(dateTime.getHours());      
      var mm = addZero(dateTime.getMinutes());
      var ss = addZero(dateTime.getSeconds());      
    
      dateTimeString = weekday[dateTime.getDay()].slice(0,3) + "," + dd + "." + MM + "." + yyyy + "  " + hh + ":" + mm + ":" + ss;
      return dateTimeString;
    };
    
    function getFormatedTime(inPktime) {
    var pktime = new Pktime(),                       
        timeString = "";        
    
      pktime.setValue(inPktime);      
      var dateTime = pktime.getDate();
            
      var hh = addZero(dateTime.getHours()-1);      
      var mm = addZero(dateTime.getMinutes());
      var ss = addZero(dateTime.getSeconds());      

      timeString = hh + ":" + mm + ":" + ss;
      return timeString;
    };
    
    function getCurrentTime() {    
    var pktime = new Pktime(),
        d = new Date();      
      pktime.setValue(d);      
      return pktime.getPktime();
    };
    
    // server - klient funkce
    
    // načtení konfigurace ze serveru             
    function getConfig() {    
    var url_1 = 'graph/getConfig';    
      $.ajax({ url: url_1,
    		async: true,        
    		dataType: "json",
        type: "post",
    		success: function(data,textStatus) {
           config = data;         
           setupLayout();
        }
      });
    };

    // načtení konfigurace alarmů ze serveru
    function getAlarmsConfig(plc) {
      var url_1 = 'graph/getAlarmsConfig?plc=' + plc;
      $.ajax({ url: url_1,
        async: true,
        dataType: "json",
        type: "post",
        success: function(data,textStatus) {
          alarmsConfig = data;
            console.log(alarmsConfig);
        }
      });
    };    
    
    function getAlarmsData(beginTime,timeAxisLength) {
      var alarmDataRequest,          

      alarmDataRequest = "";
      alarmDataRequest += '{"beginTime": ' + beginTime + ', "timeAxisLength": ' + timeAxisLength + ', "tags":[]}';

      var xmlHttp = new XMLHttpRequest();
      
      var url = '/graph/getData';      
        dataType: "json";
        xmlHttp.open("POST", url, true);
        xmlHttp.send(alarmDataRequest);
  
      if (xmlHttp.readyState != xmlHttp.DONE) {
        $('#loading').css('display','block');
      };

      xmlHttp.onreadystatechange = function() {

        if (xmlHttp.readyState == xmlHttp.DONE) {
          
          data = JSON.parse(xmlHttp.responseText.replace(/NaN/g,'null'));            
          console.log('alarm data = ' + data);
        };        
      };
    };

    function getEventsHeader(beginTime,timeAxisLength) {
      var eventsHeaderRequest,
          url_curr = new URL(document.URL);          

      var plc = url_curr.searchParams.get("plc");

      eventsHeaderRequest = "";
      eventsHeaderRequest += '{"beginTime": ' + beginTime + ', "timeAxisLength": ' + timeAxisLength + ', "tags":[]}';

      var xmlHttp = new XMLHttpRequest();
      
      var url = '/graph/getEventsHeader?plc=' + plc;      
        dataType: "json";
        xmlHttp.open("POST", url, true);
        xmlHttp.send(eventsHeaderRequest);
  
      if (xmlHttp.readyState != xmlHttp.DONE) {
        $('#loading').css('display','block');
      };

      xmlHttp.onreadystatechange = function() {

        if (xmlHttp.readyState == xmlHttp.DONE) {

          eventData = JSON.parse(xmlHttp.responseText);
        };        
      };
    };

    // získání dat ze serveru ( při reloadu,změně view)
    function getData(view,beginTime,timeAxisLength) {
    var dataRequest,
        tmpVal;
        
    dataRequest = "";
    
    dataRequest += '{"beginTime": ' + beginTime + ', "timeAxisLength": ' + timeAxisLength + ', "tags":[';
      for (var f=0;f<config.View[view].field.length;f++) {
        switch (config.View[view].field[f].type) {        
        case "alarms" :
        break;
        case "events" :
        break;
          
        default:          
          for (var s=0;s<config.View[view].field[f].signal.length;s++) {
            signal = config.View[view].field[f].signal[s];          
            
            dataRequest += '{"table": "' + signal.table + '", "column": "' + signal.column + '"';
            tmpVal = timeAxisLength/(2*chartWidth)/10;
            optTimeStep = Math.floor(tmpVal)*10;
            
            switch (signal.table) {
              case "sec5" :
                if (optTimeStep < 5) {
                  dataRequest += ', "period": 5}, '; 
                }
                else {
                  dataRequest += ', "period": ' + optTimeStep + '}, ';
                };
              break;
              case "sec10" :
                if (optTimeStep < 10) {
                  dataRequest += ', "period": 10}, '; 
                }
                else {
                  dataRequest += ', "period": ' + optTimeStep + '}, ';
                };
              break;
              case "norm" :
                if (optTimeStep < 20) {
                  dataRequest += ', "period": 20}, '; 
                }
                else {
                  dataRequest += ', "period": ' + optTimeStep + '}, ';
                };
              break;
              case "slow" :
                if (optTimeStep < 60) {
                  dataRequest += ', "period": 60}, '; 
                }
                else {
                  dataRequest += ', "period": ' + optTimeStep + '}, ';
                };
              break;
              case "xslow" :
                if (optTimeStep < 300) {
                  dataRequest += ', "period": 300}, '; 
                }
                else {
                  dataRequest += ', "period": ' + optTimeStep + '}, ';
                };
              break;            
              default :
                dataRequest += ', "period": ' + optTimeStep + '}, ';            
              break;
            };                    
          };
        break;
        };                        
      };
      
    dataRequest = dataRequest.substr(0,dataRequest.length-2);
    dataRequest += ']';
    dataRequest += '}';
    var xmlHttp = new XMLHttpRequest();
    
    var url = '/graph/getData';      
      dataType: "json";
      xmlHttp.open("POST", url, true);
      xmlHttp.send(dataRequest);

      if (xmlHttp.readyState != xmlHttp.DONE) {
        $('#loading').css('display','block');
      };

      xmlHttp.onreadystatechange = function() {

        if (xmlHttp.readyState == xmlHttp.DONE) {
          data = JSON.parse(xmlHttp.responseText.replace(/NaN/g,'null'));
          if (xmlHttp.readyState == xmlHttp.DONE) {
          data = JSON.parse(xmlHttp.responseText.replace(/NaN/g,'null'));
            if (data.tags[0].vals == null) {
              window.location = "/Account/Login";
            }
            else {
              setData();
              redrawChart(view,beginTime,markerTime,timeAxisIdx);
              redrawValues(view,markerTime,lang);
              $('#loading').css('display','none');
            };
          };
        };        
      };
    };

    // základní view
    function setupLayout() {
    var lang;
        idLimY = '';
      
      if (setting == true) {
      
        if (initial == true) {
          defConfig = config;
          initial = false;
        };
      
        if (initial == false) {
          // pres vsechny definovane fieldy          
          for (var field=1;field<=config.View[view].field.length;field++) {            
            if ((config.View[view].field[field-1].type != "binaries") || (config.View[view].field[field-1].type == undefined)) { 
              idLimY = "#f" + (field-1).toString() + "_min";
              config.View[view].field[field-1].minY = $(idLimY).val();
              idLimY = "#f" + (field-1).toString() + "_max";            
              config.View[view].field[field-1].maxY = $(idLimY).val();
            };
          };
        };
                                
        redrawChart(view,beginTime,markerTime,timeAxisIdx);                
      }
      else {
        view = lastGroup;
        lang = config.LangEnb[0];      
        //nastavení tlačítka lang na defaultní jazyk      
        setLang(lang);                                
              
        //nastavení roletky a aktuálního titulku
        setTitles(view,lang);      
  
        setMultitexts(lang);
        
        getData(view,beginTime,timeAxisLength[timeAxisIdx]*3600);
        getEventsHeader(beginTime,timeAxisLength[timeAxisIdx]*3600);

        redrawScreen();      
        redrawTitle(view,lang);      
        redrawChart(view,beginTime,markerTime,timeAxisIdx);      
        redrawTimeAxis(view,lang);
        redrawBeginTime(beginTime,lang);
        redrawLegend(view,lang);
        drawCursor(beginTime);
      };
      redrawBeginTime(beginTime,lang);      
    };
    
    function defaultLayout() {    
        location.reload(true);
        $('#settingsWin').css('visibility','hidden');
        redrawChart(view,beginTime,markerTime,timeAxisIdx);        
    };

    function setData() {    
      //přes všechny tagy
      for (var m=0;m<data.tags.length;m++) {
        if (m < MAX_CLICKMAPS) {
          chartDef[m+1]["id"] = (data.tags[m].table + '.' + data.tags[m].column);        
          if (data.tags[m]["vals"] != "null") {
            for (var v=0;v<data.tags[m]["vals"].length;v++) {
              chartDef[m+1]["times"][v] = beginTime + v*data.tags[m].period;            
            };
            chartDef[m+1]["values"] = data.tags[m]["vals"];
          }
          else {
            console.log('bad request of data');
          };
        };
      };
    };
    
    function updateConfig() {
      view = $('#group').val();
      
      var text = '';
       // nejprve vypiseme parametry fieldů                      
       for (var field=1;field<=config.View[view].field.length;field++) {
         if (config.View[view].field[field-1].type != "binaries") {
           text += ' <input style="position: absolute; top: ' + (SetPosMax[field-1]-20).toString() + 'px; padding-top: 1px; padding-bottom: 1px; width: 50px; height: 18px; margin-right: 10px; font-size: 10pt;" id="f' + (field-1).toString() + '_max" type=number min=' + (config.View[view].field[field-1].minY).toString() + ' max=' + (config.View[view].field[field-1].maxY).toString() + ' value=' + (config.View[view].field[field-1].maxY).toString() + ' step="1">';
           text += ' <input style="position: absolute; top: ' + (SetPosMin[field-1]-20).toString() + 'px; padding-top: 1px; padding-bottom: 1px; width: 50px; height: 18px; margin-right: 10px; font-size: 10pt;" id="f' + (field-1).toString() + '_min" type=number min=' + (config.View[view].field[field-1].minY).toString() + ' max=' + (config.View[view].field[field-1].maxY).toString() + ' value=' + (config.View[view].field[field-1].minY).toString() + ' step="1">';
         };
       }; 
                                                                 
      text += '<input type=button value="Set" onClick="setupLayout()" />';      
      
      $('#settingsWin').html('');
      $('#settingsWin').html(text);
      $('#settingsWin').css('visibility','visible');
    };
    
    // nastavení menu dle konfigurace   

    // nastavení multitextů s aktuálním jazykem   
    function setMultitexts(lang) {            
      //vyprázdnění předchozích multitextů
      for (var m=0;m<MAX_MULTITEXTS;m++) {
        multitexts[m]["name"] = "";
        for (var v=0;v<100;v++) {
          multitexts[m]["values"][v].idx = -99;                       
          multitexts[m]["values"][v].text = "";            
        };    
      };
      
      // nastavení multitextů pro příslušný jazyk
      for (var m=0;m<config.TextlistDef.length;m++) {
        if (m < MAX_MULTITEXTS) {
          multitexts[m]["name"] = config.TextlistDef[m].textlist;
          for (var v=0;v<config.TextlistDef[m].values.length;v++) {
            multitexts[m]["values"][v].idx = config.TextlistDef[m]["values"][v].idx;
            multitexts[m]["values"][v].text = config.TextlistDef[m]["values"][v]["text_" + lang];
          };
        };
      };
    };

    // nastavení titulků
    function setTitles(view, lang) {
      if (config.LangEnb.indexOf(lang) != -1) {
        $('#group option').remove();
        for (v=0;v<config.View.length;v++) {
          titles[v] = '';
          titles[v] = config.View[v]["name_" + lang];
          
          //nastavení roletky group
          if (v == view) {
            $('#group').append('<option value="' + v + '" selected="selected">' + titles[v] + '</option>');
          }
          else {
            $('#group').append('<option value="' + v + '" >' + titles[v] + '</option>');
          };          
        };
      }      
    };

    // funkce pro kreslení na canvas
    function redrawScreen() {
      // vyčištění a překreslení plochy mimo backCanvas      
      
      // levá část
      frontCanvas.clearRect(posX,posY,fieldLeftAdjust,iHeight);
      frontCanvas.fillStyle = colorView;
      frontCanvas.fillRect(posX,posY,fieldLeftAdjust,iHeight);
      
      // horní část
      frontCanvas.clearRect(posX,posY,iWidth,fieldTopAdjust-0.5);
      frontCanvas.fillStyle = colorView;
      frontCanvas.fillRect(posX,posY,iWidth,fieldTopAdjust+0.5);

      // pravá část
      frontCanvas.clearRect(posX+iWidth-fieldRightAdjust,posY,fieldRightAdjust-0.5,iHeight);
      frontCanvas.fillStyle = colorView;
      frontCanvas.fillRect(posX+iWidth-fieldRightAdjust,posY,fieldRightAdjust+0.5,iHeight);
      
      // spodní část
      frontCanvas.clearRect(posX+1,posY+fieldTopAdjust+chartHeight,iWidth-2,fieldBottomAdjust-0.5);
      frontCanvas.fillStyle = colorView;
      frontCanvas.fillRect(posX+1,posY+fieldTopAdjust+chartHeight,iWidth-2,fieldBottomAdjust+0.5);

      // vnější orámování
      frontCanvas.strokeStyle = colorFrames;
      frontCanvas.strokeRect(posX,posY,iWidth,iHeight);
      
      // linka pod titulkem
      frontCanvas.strokeStyle = colorFrames;
      frontCanvas.lineWidth = 1;
      
      frontCanvas.beginPath();
        frontCanvas.moveTo(posX,posY+titleBarHeight);
        frontCanvas.lineTo(posX+iWidth,posY+titleBarHeight);
        frontCanvas.stroke();
      frontCanvas.closePath();
      
      var xOff = 200;      
    
      backCanvas.fillStyle = 'green';
             
      for (x=0;x<10;x++) {        
        backCanvas.beginPath();
          backCanvas.moveTo(xOff+x*10,10);
          backCanvas.arc(xOff+x*10, 10, 3, 0, 2 * Math.PI, false);          
          backCanvas.stroke();
        backCanvas.closePath();
      };
    };    
                           
    function getViewInfo(view) {
    var viewInfo = [0,0];
    
      // fields
      for (var f=1;f<=config.View[view].field.length;f++) {

        if (config.View[view]["field"][f-1].relSize > 0) {
          viewInfo[0] += config.View[view]["field"][f-1].relSize;
          viewInfo[1]++;
        };
      };      
      return viewInfo;
    };
        
    function redrawValues(view,markerTime,lang) {
    var viewInfo = new Array(),
        fieldSumRatio = 0,
        fieldsCnt = 0,
        koefFieldHeight = 0,
        FieldPosY = 0,
        fieldSigHeight = 0,     
        value,
        lineCnt = 0,        
        lastRiseTime = 0;
      
      // fields
      viewInfo = getViewInfo(view);      
      fieldSumRatio = viewInfo[0]; 
      fieldsCnt = viewInfo[1];      
      
      if (fieldSumRatio != 0) {   //ochrana proti děleni nulou
      
        koefFieldHeight = (chartHeight-fieldBrake*(fieldsCnt-1))/fieldSumRatio;                
      };      
            
      // field
      for (var field=1;field<=config.View[view]["field"].length;field++) {        
        FieldPosY = 0;
        
        for (var j=1;j<field;j++) {
          FieldPosY += (config.View[view]["field"][j-1].relSize*koefFieldHeight)+fieldBrake;
          FieldPosY = Math.floor(FieldPosY)+0.5;
        };
        
        fieldSigHeight = Math.floor(config.View[view]["field"][field-1].relSize*koefFieldHeight);

        if (config.View[view].field[field-1].type == "binaries") {
          // secteme pocet 1 v maskach vsech signals coz bude odpovidat linkam ve fieldu
          for (s = 0; s < config.View[view].field[field - 1].signal.length; s++) {
            lineCnt += countBinaries(config.View[view].field[field - 1].signal[s].mask);
          };
        };

        var signals = new Array(),            
            signalStrVal = "",
            signalIdCfg = "",
            signalIdData = "",
            dataLastX = 0,
            dataLastY = 0,
            xPos,
            a,
            k = 0,
            cc = 0,
            lastTimeEnd,

        signals = config.View[view]["field"][field-1]["signal"];        

        // signals
        for (var x=0;x<signals.length;x++) {

          var idx = signals.length-x;

          // jde o bezna ekvidistantní data
          if ((signals[x].type != "alarm") && (signals[x].type != "event")) {

            signalIdCfg = signals[x].table + "." + signals[x].column;

            // data
            for (var d=1;d<MAX_CLICKMAPS;d++) {

              if ((signalIdCfg) == (chartDef[d].id)) {

                // times
                for (var t=0;t<chartDef[d]["times"].length;t++) {
                                
                  if ((markerTime >= chartDef[d]["times"][t]) && (markerTime < chartDef[d]["times"][t+1])) {
  
                    value = chartDef[d]["values"][t];
                    
                    if (signals[x].type != "binary") {
                  
                      // dosud nezaarchivovaná data
                      if (value > 100000) {
                          frontCanvas.fillStyle = colorBackFields;
                          frontCanvas.fillRect(posX+iWidth-fieldRightAdjust-fieldValWidth+180-0.5,posY+fieldTopAdjust+FieldPosY+fieldSigHeight-(idx)*14,95.5,14);
                          frontCanvas.fillStyle = signals[x].color;
                          frontCanvas.font = fontLegend;
                          frontCanvas.fillText("no data",posX+iWidth-fieldRightAdjust-fieldValWidth+230,posY+fieldTopAdjust+FieldPosY+fieldSigHeight-(idx-1)*14-4);
                          break;
                      };
                      
                      // ztracená data
                      if (value < -100000) {
                          frontCanvas.fillStyle = colorBackFields;
                          frontCanvas.fillRect(posX+iWidth-fieldRightAdjust-fieldValWidth+180-0.5,posY+fieldTopAdjust+FieldPosY+fieldSigHeight-(idx)*14,95.5,14);
                          frontCanvas.fillStyle = signals[x].color;
                          frontCanvas.font = fontLegend;
                          frontCanvas.fillText("missing",posX+iWidth-fieldRightAdjust-fieldValWidth+230,posY+fieldTopAdjust+FieldPosY+fieldSigHeight-(idx-1)*14-4);
                          break;
                      };
                    
                    }
                    // zatim bez detekce chybejicich dat
                    else {
                      if ((value > 100000) || (value < -100000)) {
                        frontCanvas.fillStyle = colorBackFields;
                        frontCanvas.fillRect(posX+iWidth-fieldRightAdjust-fieldValWidth+180-0.5,posY+fieldTopAdjust+FieldPosY,95.5,fieldSigHeight);
                      break;
                      }; 
                    };
                  
                    switch (signals[x].type) {

                      // typ číslo
                      case "analog" :
                        for (var m=1;m<MAX_CLICKMAPS;m++) {
                          if (chartDef[m]["id"] == signalIdCfg) {
                            if (chartDef[m]["visibility"] == "true") {
                              value = value - chartDef[m].offset;
                            }
                            else {
                              value = undefined;
                            };
                          };
                        };
  
                        if (Math.abs(chartDef[0].offset - markerTime) <= 5) {
                          value = 0;
                        }; 
                        
                        // získání numerické hodnoty (typ string) s maskou
                        if (value != undefined) {
                          signalStrVal = value.toFixed(signals[x].decimal);
                        }
                        else {
                          signalStrVal = "- - -";
                        };                     
    
                        frontCanvas.font = fontVals;
                        xPos = posX+iWidth-fieldRightAdjust-fieldValWidth+230;
                        frontCanvas.fillStyle = colorBackFields;
                        frontCanvas.fillRect(posX+iWidth-fieldRightAdjust-fieldValWidth+180-0.5,posY+fieldTopAdjust+FieldPosY+fieldSigHeight-(idx)*14.3,95.5,14);
                        
                        frontCanvas.fillStyle = signals[x].color;
                        frontCanvas.fillText(signalStrVal,xPos,posY+fieldTopAdjust+FieldPosY+fieldSigHeight-(idx-1)*14-4);                                            
                      break;
                                          
                      // typ multitext
                      case "multitext" :                      
                        signalTextList = signals[x].textlist;
                        for (var s=1;s<MAX_CLICKMAPS;s++) {
                          if (chartDef[s]["id"] == signalIdCfg) {                                                    
                            if (chartDef[s]["visibility"] == "true") {
                              // vyhledání multitextu                                                    
                              for (var m=0;m<MAX_MULTITEXTS;m++) {
                                if ((multitexts[m].name == signals[x].textlist) && (multitexts[m].name != "")) {
                                  // vyhledání textu dle hodnoty
                                  for (var v=0;v<multitexts[m].values.length;v++) {                                                                  
                                    if (multitexts[m]["values"][v].idx == value) {                                    
                                      signalStrVal = multitexts[m]["values"][v].text;
                                      break;                                    
                                    }
                                    else {                                    
                                      signalStrVal = "undefined";
                                    };
                                  };
                                  break;
                                };
                              };
                              break;
                            }
                            else {signalStrVal = "- - -";
                            };
                          };
                        };
                        
                        frontCanvas.font = fontLegend;
                        xPos = posX+iWidth-fieldRightAdjust-fieldValWidth+180;
                        frontCanvas.fillStyle = colorBackFields;
                        frontCanvas.fillRect(posX+iWidth-fieldRightAdjust-fieldValWidth+180-0.5,posY+fieldTopAdjust+FieldPosY+fieldSigHeight-(idx)*14.3,95.5,14);
                        
                        frontCanvas.fillStyle = signals[x].color;
                        frontCanvas.fillText(signalStrVal,xPos,posY+fieldTopAdjust+FieldPosY+fieldSigHeight-(idx-1)*14-4);                      
                      break;
  
                      // typ binary
                      case "binary" :
                        for (var m=1;m<MAX_CLICKMAPS;m++) {
                          if (chartDef[m]["id"] == signalIdCfg) {
  
                            if (value != undefined) {
                              frontCanvas.font = fontVals;
                              xPos = posX+iWidth-fieldRightAdjust-fieldValWidth+230;
        
                              for (bit=0;bit<=15;bit++) {
                                if (signals[x].mask[15-bit] == "1") {
                                
                                  // prirazeni barvy
                                  if (cc < 32) {
                                    if (lineCnt != 0) {
                                      signalStrVal = (maskingBinaries(value,bit)).toFixed();
                                      frontCanvas.fillStyle = colorBackFields;
                                      frontCanvas.fillRect(posX+iWidth-fieldRightAdjust-fieldValWidth+180-0.5,posY+fieldTopAdjust+FieldPosY+fieldSigHeight-((cc+1)*fieldSigHeight/lineCnt),95.5,fieldSigHeight/lineCnt);
                                      frontCanvas.fillStyle = colors[cc];
                                      frontCanvas.fillText(signalStrVal,xPos,posY+fieldTopAdjust+FieldPosY+fieldSigHeight-(cc*fieldSigHeight/lineCnt)-4);
                                    };
                                    cc++;
                                  }
                                  else {
                                    cc = 0;
                                  };
                                };
                              };
                            };                     
                          };
                        };
                      break;
                    };                  
                    break;
                  };
                };
              };
            };
          }
          // událostní data
          else {
            switch (signals[x].type) {
              case "alarm" :
                for (a=0;a<alarmData.alarms.length;a++) {
                  if (signals[x].alarmFilter.indexOf(alarmData.alarms[a].id) != -1) {
                    if (Math.abs(alarmData.alarms[a].riseTime-markerTime) <= 120) {
                      if ((alarmData.alarms[a].riseTime) == lastRiseTime) {
                        k++;
                      }
                      else {
                        k=0;
                      };

                      xPos = posX+iWidth-fieldRightAdjust-fieldValWidth+5;
                      yPos = posY+fieldTopAdjust+FieldPosY+fieldSigHeight-10-k*10;                      

                      signalStrVal = alarmsConfig[lang][alarmData.alarms[a].id];
                      
                      // vypsani textu alarmu
                      frontCanvas.font = fontLegend;
                      frontCanvas.fillStyle = "#FF0000";
                      frontCanvas.fillText(signalStrVal,xPos,yPos);

                      lastRiseTime = alarmData.alarms[a].riseTime;
                      break;
                    }
                    else {
                      frontCanvas.fillStyle = colorBackFields;
                      frontCanvas.fillRect(posX+iWidth-335,posY+fieldTopAdjust+FieldPosY,315,fieldSigHeight);
                    };
                  };
                };
              break;
            	case "event":
								if (signals[x].eventFilter != undefined) {
            			if (signals[x].eventFilter == "all") {
            				for (e = 0; e < eventData.events.length; e++) {            			
            					if (Math.abs(eventData.events[e].TimeStart - markerTime) <= 120) {

            						xPos = posX + iWidth - fieldRightAdjust - fieldValWidth + 5;
            						yPos = posY + fieldTopAdjust + FieldPosY + 20;

            						for (ec = 0; ec < eventsConfig.recordTypes.length ; ec++) {
            							if (eventsConfig.recordTypes[ec].id == eventData.events[e].RecordType) {
            								signalStrVal = eventsConfig.recordTypes[ec].label + " (Batch : " + eventData.events[e].BatchNo + ")";
            							};
            						};

            						// vypsani textu udalosti
            						frontCanvas.font = fontLegend;
            						frontCanvas.fillStyle = "#00FFFF";
            						frontCanvas.fillText(signalStrVal, xPos, yPos);

            						break;
            					}
            					else {
            						frontCanvas.fillStyle = colorBackFields;
            						frontCanvas.fillRect(posX + iWidth - 335, posY + fieldTopAdjust + FieldPosY, 315, fieldSigHeight);
            					};            			
            				};
            				break;
            			}
            			else {
            				for (e = 0; e < eventData.events.length; e++) {
            					if (signals[x].eventFilter.indexOf(eventData.events[e].RecordType) != -1) {
            						//if (Math.abs(eventData.events[e].TimeStart - markerTime) <= 120) {
            							xPos = posX + iWidth - fieldRightAdjust - fieldValWidth + 5;
            							            							
            							for (ec = 0; ec < eventsConfig.recordTypes.length ;ec++) {
            								if (eventsConfig.recordTypes[ec].id == eventData.events[e].RecordType) {                              
                              for (c=0;c<3;c++) {
                                if (eventTmp[c].BatchNo == eventData.events[e].BatchNo) {
                                  yPos = posY + fieldTopAdjust + FieldPosY + 15 + 15*c;
                                  break;
                                }
                                else {
                                  yPos = posY + fieldTopAdjust + FieldPosY + 15;
                                };
                              };
            									signalStrVal = eventsConfig.recordTypes[ec].label + " (Batch : " + eventData.events[e].BatchNo + ")";
            								};
            							};

            							// vypsani textu udalosti
            							frontCanvas.font = fontLegend;
                          frontCanvas.fillStyle = "#FFFFFF";
            							frontCanvas.fillText(signalStrVal, xPos, yPos);
            							break;
            						/*}
            						else {
            							frontCanvas.fillStyle = colorBackFields;
            							frontCanvas.fillRect(posX + iWidth - 335, posY + fieldTopAdjust + FieldPosY, 315, fieldSigHeight);
                        };*/
            					};
            				};
            			};
								};
							};
						};
          };
        };
      };

    function countBinaries(mask) {
    var count = 0;
      for (i = 0; i < mask.length; i++) {
        if (mask.charAt(i) == "1") {
            count++;
        };
      };        
    return count;
    };
    
    function maskingBinaries(value,bit) {      
      mask = 0x01;
      mask = mask << bit;      
      if ((value & mask) > 0) {
        return 1;        
      }
      else {
        return 0;
      };
    };
    
    function redrawChart(view, beginTime, markerTime, timeAxisIdx) {
    var viewInfo = new Array(),
        fieldsCnt = 0,
        fieldSumRatio = 0,        
        koefFieldHeight = 0,        
        FieldPosY = 0,
        stepHorzGrid = 0,
        stepVertGrid = 0,
        realStep = 0,
        fieldSigHeight = 0,        
        k = 0,
        offsetY = 0,
        lineCnt,
        yValue = 0,
        lastBatchNos = ""; //string broběhlych Batchů v rámci daného view

      // fields
      viewInfo = getViewInfo(view);
      fieldSumRatio = viewInfo[0];
      fieldsCnt = viewInfo[1];
      
      // vyčištění y-osy grafu
      frontCanvas.fillStyle = colorView;
      frontCanvas.fillRect(posX+1,posY+fieldBottomAdjust-10,39,chartHeight);

      // vyčištění plochy grafu      
      signalCanvas.clearRect(posX+1,posY+fieldTopAdjust,chartWidth+fieldLeftAdjust,chartHeight);
      backCanvas.clearRect(posX+1,posY+fieldTopAdjust,chartWidth+fieldLeftAdjust,chartHeight);
      
      if (fieldSumRatio != 0) { //ochrana proti děleni nulou
        // pixelů na jednotku FieldRatio
        koefFieldHeight = (chartHeight-fieldBrake*(fieldsCnt-1))/fieldSumRatio;                                
      };
      
      for (var field=1;field<=config.View[view]["field"].length;field++) {        
        FieldPosY = 0;
        for (var j=1;j<field;j++) {
         // offset v pixelech příslušného fieldu
         FieldPosY += (config.View[view]["field"][j-1].relSize*koefFieldHeight)+fieldBrake;
         FieldPosY = Math.floor(FieldPosY)+0.5;
        };

        // výška příslušného fieldu v pixelech
        fieldSigHeight = Math.floor(config.View[view]["field"][field-1].relSize*koefFieldHeight);
        
        // y-osa
        switch (config.View[view].field[field-1].type) {
        case "binaries" :
          // inicializace
          lineCnt = 0;
          
          // fields
          backCanvas.fillStyle = colorBackFields;
          backCanvas.fillRect(posX + fieldLeftAdjust, posY + fieldTopAdjust + FieldPosY, chartWidth, fieldSigHeight);

          // secteme pocet 1 v maskach vsech signals coz bude odpovidat linkam ve fieldu
          for (s = 0; s < config.View[view].field[field - 1].signal.length; s++) {
            lineCnt += countBinaries(config.View[view].field[field - 1].signal[s].mask);
          };
          
          for (var k = 0; k <= lineCnt; k++) {

            stepVertGrid = 2;
            fieldRange = 2 * lineCnt;

            // značky - body
            frontCanvas.strokeStyle = BLACK;
            frontCanvas.lineWidth = 1;

            frontCanvas.beginPath();
            frontCanvas.moveTo(posX + fieldLeftAdjust - 3, Math.floor(posY + fieldTopAdjust + FieldPosY + fieldSigHeight - (k * stepVertGrid * (fieldSigHeight / fieldRange))) + 0.5);
            frontCanvas.lineTo(posX + fieldLeftAdjust, Math.floor(posY + fieldTopAdjust + FieldPosY + fieldSigHeight - (k * stepVertGrid * (fieldSigHeight / fieldRange))) + 0.5);
            frontCanvas.stroke();
            frontCanvas.closePath();

            if (k != 0) {
              // mřížka
              backCanvas.strokeStyle = colorGrid;
              backCanvas.lineWidth = 1;

              backCanvas.beginPath();
              backCanvas.moveTo(posX + fieldLeftAdjust + 0.5, Math.floor(posY + fieldTopAdjust + FieldPosY + fieldSigHeight - (k * stepVertGrid * (fieldSigHeight / fieldRange))) + 0.5);
              backCanvas.lineTo(posX + fieldLeftAdjust + chartWidth + 0.5, Math.floor(posY + fieldTopAdjust + FieldPosY + fieldSigHeight - (k * stepVertGrid * (fieldSigHeight / fieldRange))) + 0.5);
              backCanvas.stroke();
              backCanvas.closePath();
            };

            // značky - čísla
            frontCanvas.fillStyle = BLACK;
            frontCanvas.font = fontAxis;

            yValue = offsetY - (-k * stepVertGrid);
          };
          break;
        
        case "alarms" :
          // fields
          backCanvas.fillStyle = BLACK;
          backCanvas.fillRect(posX + fieldLeftAdjust, posY + fieldTopAdjust + FieldPosY, chartWidth, fieldSigHeight);
          break;
        
        case "events" :
          // fields
        	backCanvas.fillStyle = BLACK;
          backCanvas.fillRect(posX + fieldLeftAdjust, posY + fieldTopAdjust + FieldPosY, chartWidth, fieldSigHeight);

          // vyplneni souradnic daneho fieldu pro naslednou operaci mouseDown
          chartDef[50]["coords"][1] = posY + fieldTopAdjust + FieldPosY;
          chartDef[50]["coords"][3] = posY + fieldTopAdjust + FieldPosY + fieldSigHeight;
          break;
        
        default :
          // fields
          backCanvas.fillStyle = colorBackFields;
          backCanvas.fillRect(posX + fieldLeftAdjust, posY + fieldTopAdjust + FieldPosY, chartWidth, fieldSigHeight);

          // minimalní limitní hodnota fieldu (SETTINGS)
          offsetY = config.View[view]["field"][field-1].minY;
  
          // maximální limitní hodnota fieldu (SETTINGS)
          fieldRange = config.View[view]["field"][field-1].maxY;

          // krok vertikálního rastru - relativní
          realStep = ((fieldRange-offsetY)/7);

          for (var r=12;r>=0;r--) {
            // testování dle předdefinovaného pole kroků rastru - hodnota
            if (stepGridY[r] > realStep) {
              // krok vertikálního rastru - reálná hodnota
              stepVertGrid = stepGridY[r];
            };
          };

          SetPosMax[field-1] = posY+fieldTopAdjust+FieldPosY;
          SetPosMin[field-1] = posY+fieldTopAdjust+FieldPosY+fieldSigHeight-fieldBreakVals-3;
  
          if (stepVertGrid != 0) {
            // počet linek rastru
            lineCnt = Math.floor((fieldRange-offsetY)/stepVertGrid);
          };

          for (var k = 0; k <= lineCnt; k++) {
            if (fieldRange != 0) { //ochrana před dělením nulou            
              if (fieldRange != (offsetY-(-k*stepVertGrid))) { // nevykreslovat horní limit - mřížku, bod ani číslo

                // značky - body
                frontCanvas.strokeStyle = BLACK;
                frontCanvas.lineWidth = 1;

                frontCanvas.beginPath();
                frontCanvas.moveTo(posX+fieldLeftAdjust-3, Math.floor(posY+fieldTopAdjust+FieldPosY+fieldSigHeight-(k*stepVertGrid*(fieldSigHeight/(fieldRange-offsetY))))+0.5);
                frontCanvas.lineTo(posX+fieldLeftAdjust, Math.floor(posY+fieldTopAdjust+FieldPosY+fieldSigHeight-(k*stepVertGrid*(fieldSigHeight/(fieldRange-offsetY))))+0.5);
                frontCanvas.stroke();
                frontCanvas.closePath();

                if (k != 0) {
                  // mřížka
                  backCanvas.strokeStyle = colorGrid;
                  backCanvas.lineWidth = 1;

                  backCanvas.beginPath();
                  backCanvas.moveTo(posX + fieldLeftAdjust + 0.5, Math.floor(posY + fieldTopAdjust + FieldPosY + fieldSigHeight - (k * stepVertGrid * (fieldSigHeight / (fieldRange - offsetY)))) + 0.5);
                  backCanvas.lineTo(posX + fieldLeftAdjust + chartWidth + 0.5, Math.floor(posY + fieldTopAdjust + FieldPosY + fieldSigHeight - (k * stepVertGrid * (fieldSigHeight / (fieldRange - offsetY)))) + 0.5);
                  backCanvas.stroke();
                  backCanvas.closePath();
                };

                // značky - čísla
                frontCanvas.fillStyle = BLACK;
                frontCanvas.font = fontAxis;

                yValue = offsetY - (-k * stepVertGrid);

                // pixelová délka Y-hodnoty rastru
                numLength = ((offsetY - (-k * stepVertGrid)).toString()).length * 6 + 5;      // 6 - délka jednoho znaku, 5 - pravé odsazení od fieldu

                frontCanvas.fillText(yValue, posX + fieldLeftAdjust - numLength, posY + fieldTopAdjust + FieldPosY + fieldSigHeight - (k * stepVertGrid * (fieldSigHeight / (fieldRange - offsetY))) - 2);
              };
            };
          };
          break;
        };
                
        var imgVisible = document.getElementById("signal_visible"),
            imgEvent = document.getElementById("event_point"),
            signals = new Array(),
            signal = new Array(),
            signalStrVal = "",
            signalIdCfg = "",
            signalIdData = "",
            dataLastX,
            dataLastY,
            dataStartMissingX,
            dataStartMissingY,
            dataEndMissingX,
            dataEndMissingY,
            idx = 0,
            cc = 0,
            lastRiseTime = 0,
            lastTimeEnd = 0,
						fin = 1,
            k = 0,
						i = 0;

        signals = config.View[view]["field"][field-1]["signal"];

        // rozbor parametrů jednotlivých signálů
        for (var x=0;x<signals.length;x++) {
          var idx = signals.length-x;

          if ((signals[x].type == "alarm") || (signals[x].type == "event")) {
            switch (signals[x].type) {          
              case "alarm" :
                // dekodujeme filtr
                if (signals[x].alarmFilter != undefined) {
                  if (signals[x].alarmFilter == "all") {
                    for (a=0;a<alarmData.alarms.length;a++) {
                      if ((alarmData.alarms[a].riseTime) == lastRiseTime) {
                        k++;
                      }
                      else {
                        k = 0;
                      };
                      
                      dataX = Math.round(fieldLeftAdjust + ((alarmData.alarms[a].riseTime - beginTime)/3600)*(chartWidth/timeAxisLength[timeAxisIdx]))+0.5;                            
                      dataY = posY+fieldTopAdjust+FieldPosY+fieldSigHeight-15-k*10;
                      
                      // vykreslení kulicky
                      signalCanvas.fillStyle = "#FF0000";
                      signalCanvas.arc(dataX,dataY,3,0,2*Math.PI)
                      signalCanvas.fill();
                      signalCanvas.closePath();
                      
                      lastRiseTime = alarmData.alarms[a].riseTime;                   
                    };                        
                  }
                  else {
                    for (a=0;a<alarmData.alarms.length;a++) {
                      if (signals[x].alarmFilter.indexOf(alarmData.alarms[a].id) != -1) {
                        if ((alarmData.alarms[a].riseTime) == lastRiseTime) {
                          k++;
                        }
                        else {
                          k = 0;
                        };
                        
                        dataX = Math.round(fieldLeftAdjust + ((alarmData.alarms[a].riseTime - beginTime)/3600)*(chartWidth/timeAxisLength[timeAxisIdx]))+0.5;                            
                        dataY = posY+fieldTopAdjust+FieldPosY+fieldSigHeight-15-k*10;
                        
                        // vykreslení kulicky
                        signalCanvas.fillStyle = "#FF0000";
                        signalCanvas.arc(dataX,dataY,3,0,2*Math.PI);
                        signalCanvas.fill();
                        signalCanvas.closePath();
                        
                        lastRiseTime = alarmData.alarms[a].riseTime;
                      };
                    };
                  };
                };
                break;
            	case "event":
                k = -1;
                if (signals[x].eventFilter != undefined) {
                  if (signals[x].eventFilter == "all") {
                    for (e=0;e<eventData.events.length;e++) {
                    	fin = 1;
                    	dataX = Math.round(fieldLeftAdjust + ((eventData.events[e].TimeStart - beginTime) / 3600) * (chartWidth / timeAxisLength[timeAxisIdx])) - 0.5;

                    	if (eventData.events[e].RecordType == 10) {    // start receptury
                    		// v případě jediného nedokončeného batche inkrementujeme "k" jinak "k = 0"
                    		for (i = 0; i < 3; i++) {
                    			fin = (fin & eventTmp[i].Fin);
                    		};

                    		if (fin == 0) {
                    			k++;
                    		}
                    		else {
                    			k = 0;
                    		};

                    		// ochrana proti preteceni indexu pole eventTmp 
                    		if (k >= 3) {
                    			k = 0;
                    		};

                    		dataY = Math.round(posY + fieldTopAdjust + FieldPosY + 5 + k * 16) - 0.5;

                    		signalCanvas.drawImage(imgEvent, eventData.events[e].Variant2 * 8, 0, 8, 15, dataX + 0.5, dataY + 0.5, 8, 15);

                    		switch (eventData.events[e].Variant2) {
                    			case 0:
                    				signalCanvas.fillStyle = "#00FF00";
                    				break;
                    			case 1:
                    				signalCanvas.fillStyle = "#FFFF00";
                    				break;
                    			case 2:
                    				signalCanvas.fillStyle = "#FF0000";
                    				break;
                    		};

                    		signalCanvas.font = fontLegend;
                    		signalCanvas.fillText(eventTitles(e, eventData.events[e].RecordType), dataX + 11, dataY + 12.5);

                    		eventTmp[k].BatchNo = eventData.events[e].BatchNo;
                    		eventTmp[k].Fin = 0;

                      }
                    	else if (eventData.events[e].RecordType == 14) {    // konec receptury
                        
                        for (i=0;i<3;i++) {
                        	if (eventTmp[i].BatchNo == eventData.events[e].BatchNo) {
                        		eventTmp[i].Fin = 1;

                        		dataY = Math.round(posY + fieldTopAdjust + FieldPosY + 5 + i * 16) - 0.5;
                        		break;
                        	}
                        	else {
                        	};
                        };
                          
                        signalCanvas.drawImage(imgEvent, 24 + eventData.events[e].Variant2 * 8, 0, 8, 15, dataX - 7.5, dataY + 0.5, 8, 15);

                      };
                    };
                  }  
                  else {
                    for (e=0;e<eventData.events.length;e++) {
                      fin = 1;
                    	if (signals[x].eventFilter.indexOf(eventData.events[e].RecordType) != -1) {

                    		dataX = Math.round(fieldLeftAdjust + ((eventData.events[e].TimeStart - beginTime) / 3600) * (chartWidth / timeAxisLength[timeAxisIdx])) - 0.5;

                    		if (eventData.events[e].RecordType == 10) {    // start receptury
                          
                    			// v případě jediného nedokončeného batche inkrementujeme "k" jinak "k = 0"
                    			for (i = 0; i < 3; i++) {
                    				fin = (fin & eventTmp[i].Fin);
                    			};
                    			                                  
                    			if (fin == 0) {
                    				k++;
                    			}
                    			else {
                    				k = 0;
                    			};

                    			// ochrana proti preteceni indexu pole eventTmp 
                    			if (k >= 3) {
                    				k = 0;
                    			};

                    		  dataY = Math.round(posY + fieldTopAdjust + FieldPosY + 5 + k * 16) - 0.5;

                    		  signalCanvas.drawImage(imgEvent, eventData.events[e].Variant2 * 8, 0, 8, 15, dataX + 0.5, dataY + 0.5, 8, 15);

                    		  switch (eventData.events[e].Variant2) {
                    		  	case 0:
                              signalCanvas.fillStyle = "#00FF00";
                              signalCanvas.StrokeStyle = "#00FF00";
                    		  		break;
                    		  	case 1:
                              signalCanvas.fillStyle = "#FFFF00";
                              signalCanvas.StrokeStyle = "#FFFF00";
                    		  		break;
                    		  	case 2:
                              signalCanvas.fillStyle = "#FF0000";
                              signalCanvas.StrokeStyle = "#FF0000";
                    		  		break;
                    		  };

                          signalCanvas.font = fontLegend;
                    		  signalCanvas.fillText(eventTitles(e, eventData.events[e].RecordType), dataX + 11, dataY + 12.5);

                    			eventTmp[k].BatchNo = eventData.events[e].BatchNo;
                    			eventTmp[k].Fin = 0;

                        }
                    		else if (eventData.events[e].RecordType == 14) {    // konec receptury
                          var LengthX = 0,
                              StartX = 0;

                        	for (i=0;i<3;i++) {
                        		if (eventTmp[i].BatchNo == eventData.events[e].BatchNo) {

                              eventTmp[i].Fin = 1;
                              
                              //StartX = Math.round(fieldLeftAdjust + ((eventData.events[e].TimeStart - eventData.events[e].Variant3 - beginTime) / 3600) * (chartWidth / timeAxisLength[timeAxisIdx])) - 0.5;
                              StartX = Math.round(fieldLeftAdjust + ((eventData.events[e].TimeStart - beginTime) / 3600) * (chartWidth / timeAxisLength[timeAxisIdx])) - 0.5;
                              //LengthX = Math.round((eventData.events[e].Variant3/3600)*(chartWidth / timeAxisLength[timeAxisIdx])) - 0.5;
                              LengthX =  Math.round(chartWidth / timeAxisLength[timeAxisIdx]);

                        			dataY = Math.round(posY + fieldTopAdjust + FieldPosY + 5 + i * 16) - 0.5;
                        			break;
                        		}
                        		else {
															// konec eventu ktery zacal nekdy pred zaberem view - zatim neresim
                        			//dataY = Math.round(posY + fieldTopAdjust + FieldPosY + k * 16) - 0.5;
                        			break;
                        		};
                          };

                          signalCanvas.beginPath();
                          signalCanvas.rect(StartX-LengthX, dataY, LengthX, 16);
                          signalCanvas.stroke();

                        	signalCanvas.drawImage(imgEvent, 24 + eventData.events[e].Variant2 * 8, 0, 8, 15, dataX - 7.5, dataY + 0.5, 8, 15);
                        };
                      };
                    }; 
                  };
                };
                break;
               }; 
            }
            else {

              signalIdCfg = signals[x].table + "." + signals[x].column;
              signalCanvas.fillStyle = signals[x].color;

              for (var i=1;i<MAX_CLICKMAPS;i++) {
                if (chartDef[i]["id"] == signalIdCfg) {
                  if (chartDef[i]["visibility"] == "true") {
                    // vykreslení průběhů
                    switch (signals[x].type) {
                      case "binary" :                
                        for (bit=0;bit<signals[x].mask.length;bit++) {

                          if (signals[x].mask[15-bit] == "1") {
                            // prirazeni barvy

                            if (cc < 32) {
                              signalCanvas.strokeStyle = colors[cc];
                              signalCanvas.lineWidth = 1;
                              for (var v=0;v<chartDef[i]["values"].length;v++) {

                                if (timeAxisLength[timeAxisIdx] != 0) { //ochrana před dělením nulou
                                  dataX = Math.round(fieldLeftAdjust + ((chartDef[i]["times"][v] - beginTime)/3600)*(chartWidth/timeAxisLength[timeAxisIdx]))+0.5;                    
                                };
                                // vymaskování
                                dataY = Math.round(posY+fieldTopAdjust+FieldPosY+fieldSigHeight-(maskingBinaries(chartDef[i]["values"][v],bit))*fieldSigHeight/fieldRange-(cc)*fieldSigHeight/lineCnt)+0.5;
                                signalCanvas.beginPath();

                                if ((dataX > chartDef[0]["coords"][0]) && (dataX < chartDef[0]["coords"][2])) {
                                  signalCanvas.moveTo(dataLastX, dataLastY);
                                  signalCanvas.lineTo(dataX, dataLastY);
                                  signalCanvas.moveTo(dataX, dataLastY);
                                  signalCanvas.lineTo(dataX, dataY);
                                  signalCanvas.moveTo(dataX, dataY);
                                  signalCanvas.stroke();
                                };
                                                          
                                signalCanvas.closePath();
                                
                                if ((chartDef[i]["values"][v] > -100000)) {
                                  dataLastX = dataX;
                                  dataLastY = dataY;
                                }; 
                                                                                                      
                              };                                     
                              cc++;
                            }
                            else {
                              cc = 0;
                            };
                          };
                        };                  
                        break;                      
                      default :
                        signalCanvas.strokeStyle = signals[x].color;
                        signalCanvas.lineWidth = 1;
      
                        for (var v=0;v<chartDef[i]["values"].length;v++) {
                        
                          if (timeAxisLength[timeAxisIdx] != 0) {    //ochrana před dělením nulou
                            dataX = Math.round(fieldLeftAdjust + ((chartDef[i]["times"][v] - beginTime)/3600)*(chartWidth/timeAxisLength[timeAxisIdx]))+0.5;                    
                          };
                          
                          dataY = Math.round(posY+fieldTopAdjust+FieldPosY+fieldSigHeight-(chartDef[i]["values"][v]-offsetY)*(fieldSigHeight/(fieldRange-offsetY)))+0.5;
        
                          //začátek díry v datech                  
                          if ((v >= 1) && (chartDef[i]["values"][v-1] > -100000) && (chartDef[i]["values"][v] <= -100000)) {                                        
                            dataStartMissingX = dataLastX;
                            dataStartMissingY = dataLastY;
                          };
                          
                          //konec díry v datech
                          if ((v >= 1) && (chartDef[i]["values"][v-1] <= -100000) && (chartDef[i]["values"][v] > -100000) || (chartDef[i]["values"][0] <= -100000)) {
                            dataEndMissingX = dataX;
                            dataEndMissingY = dataY;
                            signalCanvas.moveTo(dataStartMissingX,dataStartMissingY);                    
                            signalCanvas.lineTo(dataEndMissingX,dataEndMissingY);
                            signalCanvas.stroke();
                          };
                          
                          if ((v == 0) && (chartDef[i]["values"][0] > -100000)) {
                            dataLastX = dataX;
                            dataLastY = dataY;
                          };
        
                          signalCanvas.beginPath();
        
                          if ((dataX > chartDef[0]["coords"][0]) && (dataX < chartDef[0]["coords"][2])) {
      
                            if (chartDef[i]["values"][v]*(fieldSigHeight/fieldRange) > fieldSigHeight) {
                              signalCanvas.fillStyle = signals[x].color;
                              signalCanvas.fill();
                            };
      
                            switch (signals[x].type) {
                            
                              case "analog" :
                                signalCanvas.moveTo(dataLastX,dataLastY);
                                signalCanvas.lineTo(dataX,dataY);
                                signalCanvas.stroke();
                                break;
                              
                              case "multitext" :
                                signalCanvas.moveTo(dataLastX,dataLastY);
                                signalCanvas.lineTo(dataX,dataLastY);
                                signalCanvas.moveTo(dataX,dataLastY);
                                signalCanvas.lineTo(dataX,dataY);
                                signalCanvas.moveTo(dataX,dataY);                        
                                signalCanvas.stroke();
                            };
                          };
        
                          signalCanvas.closePath();
                          
                          if ((chartDef[i]["values"][v] > -100000)) {
                            dataLastX = dataX;
                            dataLastY = dataY;
                          };
                        };
                        break;
                      };
                    };
                  };
                };
              };
        };
      };
      
      // vertikální mřížka
      stepHorzGrid = stepGridTime[timeAxisIdx]/60;
      lineCnt = Math.floor(timeAxisLength[timeAxisIdx]/stepHorzGrid);
       
      for (k=0;k<lineCnt;k++) {
        if (timeAxisLength[timeAxisIdx] != 0) { //ochrana před dělením nulou         
          
          backCanvas.strokeStyle = colorGrid;          
          backCanvas.lineWidth = 1;

          backCanvas.beginPath();
            backCanvas.moveTo(posX+fieldLeftAdjust+Math.round(k*stepHorzGrid*(chartWidth/timeAxisLength[timeAxisIdx])),posY+fieldTopAdjust);
            backCanvas.lineTo(posX+fieldLeftAdjust+Math.round(k*stepHorzGrid*(chartWidth/timeAxisLength[timeAxisIdx])),posY+fieldTopAdjust+chartHeight);
            backCanvas.stroke();
          backCanvas.closePath();
        };
      };
    };

    function eventTitles(eventDataId, recType) {

    	eventArray = new Array(10);
      detailArray = new Array(6);
      Column = "";
    	eventTitle = "";
      mask = "";
      detailIdx = 0;

    	// init
    	for (t = 0; t < eventArray.length; t++) {
    		eventArray[t] = "";
    	};

			// vyhledani masky
    	for (c = 0; c < eventsConfig.recordTypes.length; c++) {
    		if (eventsConfig.recordTypes[c].id == recType) {
    			mask = eventsConfig.recordTypes[c].mask;
    		};
    	};

    	eventArray = mask.split("$");
      eventTitle = "";
			// rozbor masky
    	for (i = 0; i < eventArray.length; i++) {

				// data
    		if (eventArray[i].charAt(0) == "@") {
          // detail
          detailIdx = eventArray[i].indexOf("#");
          
    			if (detailIdx != -1) {
            // test zda nasleduje pismeno ci cislo
            //jde o specifikaci zobrazeni ciselne hodnoty
            if ((eventArray[i].charCodeAt(detailIdx+1) >= 48) && (eventArray[i].charCodeAt(detailIdx+1) <= 57)) {

              detailArray = eventArray[i].split("#");
              valueArray = detailArray[1].split(",");
              Column = detailArray[0].replace("@", "").toString();
              eventTitle +=  " " + (eventData.events[eventDataId][Column] * valueArray[0]).toFixed(valueArray[1]) + " " + getUnits(valueArray[2]);
            }
            // jde o definici multitextu
            else {
              detailArray = eventArray[i].split("#");
              Column = detailArray[0].replace("@", "");
              eventTitle += eventsConfig[detailArray[1]][eventData.events[eventDataId][Column]];
            };
          };
        }
        // text
        else { 
          eventTitle += eventArray[i];
        };
      };  
      return eventTitle
    };

    function getUnits(unitString) {
    	var i = 0;
      lang = getLang();
      i = eventsConfig.units.langs.lastIndexOf(lang);
    	return eventsConfig.units[unitString][i];
    };
    
    function redrawTimeAxis(view,lang) {
    var date,
        time,
        viewInfo = new Array(),
        fieldsCnt = 0,
        fieldSumRatio = 0;                

      //fields
      viewInfo = getViewInfo(view);
      fieldSumRatio = viewInfo[0];
      fieldsCnt = viewInfo[1];
      
      // vyčištění plochy časové osy a 
      frontCanvas.clearRect(posX+1,posY+fieldTopAdjust+chartHeight,fieldLeftAdjust+chartWidth+fieldBreakVals,28);
      frontCanvas.fillStyle = colorView;
      frontCanvas.fillRect(posX+1,posY+fieldTopAdjust+chartHeight,fieldLeftAdjust+chartWidth+fieldBreakVals,28.5);
  
      // časová osa
      stepHorzGrid = stepGridTime[timeAxisIdx]/60;
      lineCnt = Math.floor(timeAxisLength[timeAxisIdx]/stepHorzGrid);
      
      for (var k=0;k<=lineCnt;k++) {
        if (timeAxisLength[timeAxisIdx] != 0) { //ochrana před dělením nulou
                    
          // značky - body
          frontCanvas.strokeStyle = BLACK;          
          frontCanvas.lineWidth = 1;
      
          frontCanvas.beginPath();
            frontCanvas.moveTo(posX+fieldLeftAdjust+Math.floor(k*stepHorzGrid*(chartWidth/timeAxisLength[timeAxisIdx])),posY+fieldTopAdjust+chartHeight);
            frontCanvas.lineTo(posX+fieldLeftAdjust+Math.floor(k*stepHorzGrid*(chartWidth/timeAxisLength[timeAxisIdx])),posY+fieldTopAdjust+chartHeight+3);
            frontCanvas.stroke();
          frontCanvas.closePath();
  
          // značky - čísla
          frontCanvas.fillStyle = BLACK;
          frontCanvas.font = fontAxis;
                                                                 
          datetime = (getFormatedDateTime(beginTime+k*stepHorzGrid*3600+utcBias,lang));

          date = datetime.slice(datetime.length-20,datetime.length-14);
          time = datetime.slice(datetime.length-8,datetime.length-3);              
          
          if ((stepGridTime[timeAxisIdx]) >= 1440) {
            frontCanvas.fillText(time,posX+fieldLeftAdjust+k*stepHorzGrid*(chartWidth/timeAxisLength[timeAxisIdx])-time.length*5/2,posY+fieldTopAdjust+chartHeight+13);
            frontCanvas.fillText(date,posX+fieldLeftAdjust+k*stepHorzGrid*(chartWidth/timeAxisLength[timeAxisIdx])-date.length*5/2,posY+fieldTopAdjust+chartHeight+23);
          }
          else {
            frontCanvas.fillText(time,posX+fieldLeftAdjust+k*stepHorzGrid*(chartWidth/timeAxisLength[timeAxisIdx])-time.length*5/2,posY+fieldTopAdjust+chartHeight+13);
          };
        };
      };
    };
    
    function redrawBeginTime(beginTime,lang) {
      frontCanvas.clearRect(posX+fieldLeftAdjust,posY+iHeight-fieldBottomAdjust+poleBegtimeOdsazY,fieldValWidth,16);

      frontCanvas.strokeStyle = colorFrames;      
      frontCanvas.strokeRect(posX+fieldLeftAdjust,posY+iHeight-fieldBottomAdjust+poleBegtimeOdsazY,fieldValWidth,16);

      frontCanvas.fillStyle = BLACK;               
      frontCanvas.font = fontTitle;
      frontCanvas.fillText(getFormatedDateTime(beginTime+utcBias,lang),posX+0.5+fieldLeftAdjust+5,posY+0.5+iHeight-fieldBottomAdjust+poleBegtimeOdsazY+12);
    };

    function redrawTitle(view) {      
      frontCanvas.clearRect(posX+fieldLeftAdjust,posY+fieldTopAdjust-24,350.5,23.5);

      frontCanvas.fillStyle = colorView;
      frontCanvas.fillRect(posX+fieldLeftAdjust,posY+fieldTopAdjust-24,350.5,23.5);

      frontCanvas.fillStyle = colorTitle;
      frontCanvas.font = fontTitle;      
      frontCanvas.fillText(titles[view],posX+0.5+fieldLeftAdjust,posY+0.5+fieldTopAdjust-10);
    };

    function redrawLegend(view,lang) {
    var viewInfo = new Array(),
        fieldsCnt = 0,
        fieldSumRatio = 0,
        koefFieldHeight = 0,
        FieldPosY = 0,
        fieldSigHeight = 0,
        sigGlobalIdx = 0,
        lineCnt = 0;
        
      // fields      
      viewInfo = getViewInfo(view);
      fieldSumRatio = viewInfo[0];
      fieldsCnt = viewInfo[1];
      
      // vyčištění plochy legendy
      frontCanvas.clearRect(posX+fieldLeftAdjust+chartWidth,posY+fieldTopAdjust,fieldValWidth+fieldBreakVals-1,chartHeight);
      
      // legenda
      frontCanvas.fillStyle = colorView;
      frontCanvas.fillRect(posX+fieldLeftAdjust+chartWidth,posY+fieldTopAdjust,fieldValWidth+fieldBreakVals-1,chartHeight);
      
      if (fieldSumRatio != 0) { //ochrana proti děleni nulou
        koefFieldHeight = (chartHeight-fieldBrake*(fieldsCnt-1))/fieldSumRatio;
      };
      
      sigGlobalIdx = 0;

      for (var field=1;field<=config.View[view]["field"].length;field++) {        
        FieldPosY = 0;

        for (var j=1;j<field;j++) {
         FieldPosY += (config.View[view]["field"][j-1].relSize*koefFieldHeight)+fieldBrake;
         FieldPosY = Math.floor(FieldPosY)+0.5;
        };

        fieldSigHeight = Math.floor(config.View[view]["field"][field-1].relSize*koefFieldHeight);

        switch (config.View[view].field[field-1].type) {
        case "binaries" :
          for (s = 0; s < config.View[view].field[field - 1].signal.length; s++) {
            lineCnt += countBinaries(config.View[view].field[field - 1].signal[s].mask);
          };  
          
          // legenda
          frontCanvas.fillStyle = colorBackFields;
          frontCanvas.fillRect(posX+iWidth-fieldRightAdjust-fieldValWidth,posY+fieldTopAdjust+FieldPosY,fieldValWidth,fieldSigHeight);                  
          
          var signals = new Array(),
              signal = new Array(),
              mask = "",       
              signalStrVal = "",
              signalIdCfg = "",    
              signalIdData = "",
              idx,
              cc = 0;

          signals = config.View[view]["field"][field-1]["signal"];

          // rozbor parametrů jednotlivých signálů
          for (var x=0;x<signals.length;x++) {
            for (bit=0;bit<signals[x].mask.length;bit++) {
              if (signals[x].mask[15-bit] == "1") {
                sigGlobalIdx++;
                // prirazeni barvy
                if (cc < 32) {
                  frontCanvas.fillStyle = colors[cc];
                  signalIdCfg = signals[x].table + "." + signals[x].column;
                  
                  frontCanvas.font = fontLegend;
                
                  // nastavime legendu na column name,
                  // v pripade ze je definovaný name tak pozdeji nahradime
                  signalLegend = signals[x].column + "#" + bit;
                  
                  for (n=0;n<config.NameDef.length;n++) {
                    if (config.NameDef[n].column == signals[x].column) {
                      signalLegend = config.NameDef[n]["bitsName_" + lang][bit];
                    };
                  };
                  
                  sX = Math.round(posX+iWidth-fieldRightAdjust-fieldValWidth-13)-0.5;
                  sY = Math.round(posY+fieldTopAdjust+FieldPosY+fieldSigHeight-(cc-1)*Math.floor(fieldSigHeight/lineCnt))-0.5;
                  
                  frontCanvas.fillText(signalLegend,posX+0.5+iWidth-fieldRightAdjust-fieldValWidth+5,posY+0.5+fieldTopAdjust+FieldPosY+fieldSigHeight-(cc)*Math.floor(fieldSigHeight/lineCnt)-4.5);
                
                  cc++;
                }
                else {
                  cc = 0;
                };
              };
            };
          };
        break;
        case "alarms" :            
          // legenda
          frontCanvas.fillStyle = colorBackFields;
          frontCanvas.fillRect(posX+iWidth-fieldRightAdjust-fieldValWidth,posY+fieldTopAdjust+FieldPosY,fieldValWidth,fieldSigHeight);                  
        break;

        case "events" :
          // legenda
          frontCanvas.fillStyle = colorBackFields;
          frontCanvas.fillRect(posX+iWidth-fieldRightAdjust-fieldValWidth,posY+fieldTopAdjust+FieldPosY,fieldValWidth,fieldSigHeight);
        break;
        
        default :
          // legenda
          frontCanvas.fillStyle = colorBackFields;
          frontCanvas.fillRect(posX+iWidth-fieldRightAdjust-fieldValWidth,posY+fieldTopAdjust+FieldPosY,fieldValWidth,fieldSigHeight);                  
          
          var imgVisible = document.getElementById("signal_visible"),
              signals = new Array(),
              signal = new Array(),
              mask = "",       
              signalStrVal = "",
              signalIdCfg = "";    
              signalIdData = "";
              
          signals = config.View[view]["field"][field-1]["signal"];        
          
          // rozbor parametrů jednotlivých signálů
          for (var x=0;x<signals.length;x++) {
            var idx = signals.length-x;
                      
            sigGlobalIdx++;
            
            signalIdCfg = signals[x].table + "." + signals[x].column;                    
            
            frontCanvas.fillStyle = signals[x].color;          
            frontCanvas.font = fontLegend;
            
            // nastavime legendu na column name,
            // v pripade ze je definovaný name tak pozdeji nahradime
            signalLegend = signals[x].column;
                      
            for (n=0;n<config.NameDef.length;n++){
               
              if (/*(config.NameDef[n].table == signals[x].table) && */(config.NameDef[n].column == signals[x].column)) {
                
                signalLegend = config.NameDef[n]["fullName_" + lang];              
                
                if ("undefined" === typeof config.NameDef[n]["unit_" + lang]){                
                  signalUnits = "";                
                }
                else {
                  signalUnits = config.NameDef[n]["unit_" + lang];
                };
              };
            };
  
            sX = Math.round(posX+iWidth-fieldRightAdjust-fieldValWidth-13)-0.5;
            sY = Math.round(posY+fieldTopAdjust+FieldPosY+fieldSigHeight-(idx)*14)-0.5;
            frontCanvas.fillText(signalLegend,posX+0.5+iWidth-fieldRightAdjust-fieldValWidth+5,posY+0.5+fieldTopAdjust+FieldPosY+fieldSigHeight-(idx-1)*14-4.5);
  
            // omezeni indexu
            if (sigGlobalIdx < MAX_CLICKMAPS) {                    
              chartDef[sigGlobalIdx]["id"] = signalIdCfg;
              chartDef[sigGlobalIdx]["coords"][0] = sX;
              chartDef[sigGlobalIdx]["coords"][1] = sY;
              chartDef[sigGlobalIdx]["coords"][2] = sX+11;
              chartDef[sigGlobalIdx]["coords"][3] = sY+11;
                                    
              if (chartDef[sigGlobalIdx]["visibility"] == "true") {
                frontCanvas.drawImage(imgVisible,0,0,11,11,sX+0.5,sY+0.5,11,11);
              }
              else {
                frontCanvas.drawImage(imgVisible,11,0,11,11,sX+0.5,sY+0.5,11,11);
              };
            };

            // jednotky
            if (signals[x].type == "analog") {  // typ číslo
              frontCanvas.fillStyle = signals[x].color;
              frontCanvas.font = fontLegend;
              frontCanvas.fillText(signalUnits,posX+iWidth-fieldRightAdjust-fieldValWidth+275,posY+fieldTopAdjust+FieldPosY+fieldSigHeight-(idx-1)*14-4.5);
            };
          };
        break;
        };
      };
    };
    
    function redrawMarkerTime(markerTime,lang) {
    var sign;
    
      // vyčištění plochy marker time      
      frontCanvas.clearRect(posX+iWidth-fieldRightAdjust-fieldValWidth,posY+iHeight-fieldBottomAdjust+poleBegtimeOdsazY,fieldValWidth,16);
          
      frontCanvas.fillStyle = colorBackTime;
      frontCanvas.fillRect(posX+iWidth-fieldRightAdjust-fieldValWidth,posY+iHeight-fieldBottomAdjust+poleBegtimeOdsazY,fieldValWidth,16);
      
      frontCanvas.fillStyle = colorTime;               
      frontCanvas.font = fontTitle;
      
      if (reset == true) {        
        sign = (markerTime >= chartDef[0].offset) ? " + " : " - ";
        
        frontCanvas.fillText(sign + getFormatedTime(Math.abs(markerTime-chartDef[0].offset)),posX+0.5+iWidth-fieldRightAdjust-fieldValWidth+5,posY+0.5+iHeight-fieldBottomAdjust+poleBegtimeOdsazY+12);
      }
      else {
        frontCanvas.fillText(getFormatedDateTime(markerTime+utcBias,lang),posX+0.5+iWidth-fieldRightAdjust-fieldValWidth+5,posY+0.5+iHeight-fieldBottomAdjust+poleBegtimeOdsazY+12);
      };
            
    };
       
    function separate(string,separator,idx) {        
    var items = new Array();
        item = "";
        
      if (idx == -1) {   
        items = string.split(separator);              
        return items;
      }
      else {
      
        items = string.split(separator);
        item = items[idx];                   
        return item;
      };              
    };         
    
   // tlačítka menu 

   // event shift
   var shiftTime; 
   var isShift = false;
   var shiftDelta = 250; 
  
   function backShift() {     
     if (isShift == false) {
       agregBeginTime = beginTime;
     }
     
     agregBeginTime -= (timeAxisLength[timeAxisIdx]*3600)/2;
    
     shiftTime = new Date();
     if (isShift == false) {
         isShift = true;
           setTimeout(shiftEnd(), shiftDelta);
     };
    
     function shiftEnd() {
       if (new Date() - shiftTime < shiftDelta) {
         setTimeout(shiftEnd, shiftDelta);
         isShift = true;
       } else {
         agregShift();
       };     
     };
   }; 
   
   function fwdShift() {   
     if (isShift == false) {
       agregBeginTime = beginTime;
     }
               
     agregBeginTime += (timeAxisLength[timeAxisIdx]*3600)/2;
     
     shiftTime = new Date();
     if (isShift == false) {
         isShift = true;
           setTimeout(shiftEnd(), shiftDelta);
     };
    
     function shiftEnd() {
       if (new Date() - shiftTime < shiftDelta) {
         setTimeout(shiftEnd, shiftDelta);
         isShift = true;
       } else {
         agregShift();
       };     
     };     
   };

   function agregShift() {     
     view = $('#group').val();
     lang = getLang();
     beginTime = agregBeginTime;
     getData(view,beginTime,timeAxisLength[timeAxisIdx]*3600);     
     getEventsHeader(beginTime,timeAxisLength[timeAxisIdx]*3600);

     redrawChart(view,beginTime,markerTime,timeAxisIdx);
     redrawBeginTime(beginTime,lang);
     redrawTimeAxis(view,lang);
     drawCursor(markerTime);
     isShift = false;
   };
     
   function narrow() {
     view = $('#group').val();
     lang = getLang();    
     timeAxisIdx = (timeAxisIdx >= 9) ? 9 : timeAxisIdx+1;
     getData(view,beginTime,timeAxisLength[timeAxisIdx]*3600);
     getEventsHeader(beginTime,timeAxisLength[timeAxisIdx]*3600);

     shiftLength(timeAxisIdx);
     redrawChart(view,beginTime,markerTime,timeAxisIdx);
     redrawBeginTime(beginTime,lang);
     redrawTimeAxis(view,lang);
     drawCursor(markerTime);
   };
   
   function extend() {
     view = $('#group').val();
     lang = getLang();     
     timeAxisIdx = (timeAxisIdx <= 0) ? 0 : timeAxisIdx-1;
     getData(view,beginTime,timeAxisLength[timeAxisIdx]*3600);
     getEventsHeader(beginTime,timeAxisLength[timeAxisIdx]*3600);

     shiftLength(timeAxisIdx);     
     redrawChart(view,beginTime,markerTime,timeAxisIdx);
     redrawBeginTime(beginTime,lang);
     redrawTimeAxis(view,lang);
     drawCursor(markerTime);     
   };
   
   function backDay() {
     if (isShift == false) {
       agregBeginTime = beginTime;
     }
        
     agregBeginTime -= 86400;
     
     shiftTime = new Date();
     if (isShift == false) {
         isShift = true;
           setTimeout(shiftEnd(), shiftDelta);
     };
    
     function shiftEnd() {
       if (new Date() - shiftTime < shiftDelta) {
         setTimeout(shiftEnd, shiftDelta);
         isShift = true;
       } else {
         agregShift();
       };     
     };
     
   };
   
   function fwdDay() {
    if (isShift == false) {
       agregBeginTime = beginTime;
     }
        
     agregBeginTime += 86400;
     
     shiftTime = new Date();
     if (isShift == false) {
         isShift = true;
           setTimeout(shiftEnd(), shiftDelta);
     };
    
     function shiftEnd() {
       if (new Date() - shiftTime < shiftDelta) {
         setTimeout(shiftEnd, shiftDelta);
         isShift = true;
       } else {
         agregShift();
       };     
     };
   };
   
   function zoomSignal(val) {
     switch (val) {
     case "zoom":
       zoom = true;       
       $('#zoom').attr('value',"ZOOM");        
     break;
     case "ZOOM":       
       zoom = false;
       eraseActiveCanvas(0);
       $('#zoom').attr('value',"zoom");        
     break;               
     };   
   };      

   function resetValue(val) {
     var t;
     switch (val) {
       case ">0<":
         // vyhledani nejblizsiho vzorku zpetne v case - pro jednotlive signaly muze byt v různém čase !
         for (var m=1;m<MAX_CLICKMAPS;m++) {
           var t=0;
           while ((chartDef[m]["times"][t] != undefined) && (t<=chartDef[m]["times"].length) && (markerTime > chartDef[m]["times"][t])) {
             t++;
           };
             chartDef[m].offset = chartDef[m]["values"][t-1];
             reset = true;
             chartDef[0].offset = markerTime;
             view = $('#group').val();
             lang = getLang();
             redrawMarkerTime(markerTime,lang);
             redrawValues(view,markerTime,lang);
             $('#reset').attr('value',">>0<<");
         };
       break;
       case ">>0<<":       
         reset = false;       
         for (var m=0;m<MAX_CLICKMAPS;m++) {               
           chartDef[m].offset = 0;         
         };
         $('#reset').attr('value',">0<");       
         view = $('#group').val();
         lang = getLang();
         redrawMarkerTime(markerTime,lang)
         redrawValues(view,markerTime,lang);        
       break;               
     };     
   };   
   
   function refresh() {
     view = $('#group').val();
     lang = getLang();     
     beginTime += 1;
     getData(view,beginTime,timeAxisLength[timeAxisIdx]*3600);
     getEventsHeader(beginTime,timeAxisLength[timeAxisIdx]*3600);

     redrawChart(view,beginTime,markerTime,timeAxisIdx);
     redrawBeginTime(beginTime,lang);
     redrawTimeAxis(view,lang);
     drawCursor(markerTime);
   };

   function changeGroup(view) {
     var url_curr = new URL(document.URL);
     var plc = "";

     for (m=1;m<=MAX_CLICKMAPS;m++) {       
      chartDef[m]["visibility"] = "true";         
     };
     
     lastGroup = view;
     
     getData(view,beginTime,timeAxisLength[timeAxisIdx]*3600);

     var plc = url_curr.searchParams.get("plc");

     getEventsHeader(beginTime,timeAxisLength[timeAxisIdx]*3600);
     getAlarmsConfig("Bread%20fermentation");

     lang = getLang();
     redrawScreen();
     redrawTitle(view,lang);
     redrawChart(view,beginTime,markerTime,timeAxisIdx);
     redrawTimeAxis(view,lang);
     redrawValues(view,markerTime,lang);
     redrawLegend(view,lang);
     redrawBeginTime(beginTime,lang);
     drawCursor(markerTime);
   };

   function changeLang() {
   var lang = '',
       idx = 0;

     view = $('#group').val();
     lang = getLang();
     idx = config.LangEnb.indexOf(lang);

     if (idx < config.LangEnb.length-1) {
       $('#lang').attr('value',"lang:" + config.LangEnb[idx+1]);
       setTitles(view,config.LangEnb[idx+1]);
       setMultitexts(config.LangEnb[idx+1]);
       redrawTitle(view,config.LangEnb[idx+1]);       
       redrawLegend(view,config.LangEnb[idx+1]);
       redrawValues(view,markerTime,config.LangEnb[idx+1]);
       redrawMarkerTime(markerTime,config.LangEnb[idx+1]);
       redrawBeginTime(beginTime,config.LangEnb[idx+1]);       
     }
     else {
       $('#lang').attr('value',"lang:" + config.LangEnb[0]);
       setTitles(view,config.LangEnb[0]);
       setMultitexts(config.LangEnb[0]);
       redrawTitle(view,config.LangEnb[0]);
       redrawLegend(view,config.LangEnb[0]);
       redrawValues(view,markerTime,config.LangEnb[0]);
       redrawMarkerTime(markerTime,config.LangEnb[0]);
       redrawBeginTime(beginTime,config.LangEnb[0]);
     };
   };

   //nastavení tlačítka jazyk vstupem je string "CZ", "EN", apod.
   function setLang(lang) {   
     if (config.LangEnb.indexOf(lang) != -1) {
       $('#lang').attr('value',"lang:" + lang)
     }
     else {
       $('#lang').attr('value',"lang:" + config.LangEnb)
     };
   };

   //získání aktuálně nastaveného jazyka
   function getLang() {
   var lang;
   
     lang = $('#lang').val();
     lang = lang.substring(lang.length-2,lang.length);
     if (config.LangEnb.indexOf(lang) != -1) {
     	return lang;
     }
     else return config.LangEnb;
   };

   function changeZone(zone) {
     switch (zone) {
     case "zone:CET":
       utcBias += 3*3600;
       $('#zone').attr('value',"zone:Moscow");
       break;                                                     
     case "zone:Moscow":
       utcBias -= 8*3600;
       $('#zone').attr('value',"zone:New York");       
       break;
     case "zone:New York":
       utcBias += 4*3600;
       $('#zone').attr('value',"zone:UTC");
       break;
     case "zone:UTC":
       utcBias += 3600;
       $('#zone').attr('value',"zone:CET");
       break;       
     break;
     
     };

     view = $('#group').val();
     lang = getLang();
     redrawScreen();
     redrawTitle(view,lang);          
     redrawChart(view,beginTime,markerTime,timeAxisIdx);
     redrawBeginTime(beginTime,lang);
     redrawTimeAxis(view,lang);
     drawCursor(markerTime);
   };
   
   function goDateTime(dd,MM,yyyy) {
     $('#calendar').css('visibility','hidden');     
     beginTime = parseInt((Date.UTC(yyyy,MM-1,dd,-1,0,0,0) - Date.UTC(2000,0,1,0,0,0,0))/1000,10);
     
     view = $('#group').val();
     lang = $('#lang').val();
     timeAxisIdx = 6;
     getData(view,beginTime,timeAxisLength[timeAxisIdx]*3600);
     getEventsHeader(beginTime,timeAxisLength[timeAxisIdx]*3600);

     redrawChart(view,beginTime,markerTime,timeAxisIdx);
     redrawTimeAxis(view,lang);
     redrawBeginTime(beginTime,lang);
     drawCursor(markerTime);     
   };

   function maxDays(mm,yyyy) {
   var mDay;
   	 if((mm == 3) || (mm == 5) || (mm == 8) || (mm == 10)) {
   	 mDay = 30;
   	 }
     else {
       mDay = 31
    	 if(mm == 1) {
     	   if (yyyy/4 - parseInt(yyyy/4) != 0){
     		   mDay = 28
     		 }
    	   else {
     		   mDay = 29
    		 };
       };
     };
   return mDay;
   };
    
   function changeBg(id) {
   var selDD,
       selMM,
       selYYYY;
            	
     if (eval(id).className != "c3") {
       selDD = document.getElementById(id).innerHTML;
       selMM = $('#month').prop('selectedIndex')+1;
       selYYYY = $('#year').val();
       eval(id).style.backgroundColor = "yellow";
       goDateTime(selDD,selMM,selYYYY);
     };
   };
    
   function calendar() {
   var now = new Date,
       dd = now.getDate(),
       mm = now.getMonth(),
       dow = now.getDay(),
       yyyy = now.getFullYear(),  
       arrY = new Array(),       
       text = "";
        
     for (ii=0;ii<=4;ii++) {
       arrY[ii] = yyyy - 4 + ii;
     };

     if ($('#calendar').css('visibility') == "visible") {
       $('#calendar').css('visibility','hidden');
       return;
     }; 
     
     $('#calendar').css('visibility','hidden');
          
     var lang = getLang();       	

     switch (lang) {
     case "EN":
        arrM = monthsEN;      
        arrD = daysEN1;
        break;
      case "CZ":
        arrM = monthsCZ;
        arrD = daysCZ1;
        break;
      case "DE":
        arrM = monthsDE;
        arrD = daysDE1;
        break;
      case "PL":
        arrM = monthsPL;
        arrD = daysPL1;
        break;
      case "RU":
        arrM = monthsRU;
        arrD = daysRU1;
        break;    
      };

     text += "<form name=calForm>"
     text += "<table style=\"border-style: groove; border-width: 1px;\">"
   
     // měsíc
     text += "<tr><td>"
     text += "<table width=100% style=\"border-style: none; border-width: 1px;\"><tr>"
     text += "<td align=left>"
     text += "<select id=\'month\' name=selMonth onChange='changeCal()'>"
     for (ii=0;ii<=11;ii++) {
       if (ii==mm) {
         text += "<option value= " + ii + " Selected>" + arrM[ii] + "</option>"
    	   }
    	   else {
    	     text += "<option value= " + ii + ">" + arrM[ii] + "</option>"
    	   };
       };
     text += "</select>"
     text += "</td>"
    
     // rok
     text += "<td align=right>"
     text += "<select id=\'year\' name=selYear onChange='changeCal()'>"
    
     for (ii=4;ii>=0;ii--) {
       if (ii==4) {
         text += "<option value= " + arrY[ii] + " Selected>" + arrY[ii] + "</option>"
      	 }
      	 else {
      	   text += "<option value= " + arrY[ii] + ">" + arrY[ii] + "</option>"
      	 };
       };
    
     text += "</select>"
     text += "</td>"
     text += "</tr></table>"
     text += "</td></tr>"
    
     // názvy dnů
     text += "<tr><td>"
     text += "<table style=\"border-style: groove; border-width: 1px;\">"
     text += "<tr>"
     for (ii=0;ii<=6;ii++) {
       text += "<td align=center;><span style=\"color: black; width: 30px; height: 16px; font: bold 13px Arial;\">" + arrD[ii] + "</span></td>"		
     };	
     text += "</tr>"
    
     // dny
     aa = 0;
     for (kk=0;kk<=5;kk++) {
       text += "<tr>"
       for (ii=0;ii<=6;ii++) {      		  
         text += "<td align=center><span id=sp" + aa + " onClick='changeBg(this.id)'>1</span></td>"
    	   aa += 1;
       };
       text += "</tr>"
     };
     text += "</table>"
     text += "</td></tr>"
     text += "</table>"
     text += "</form>"     
     $('#calendar').html(text);
     $('#calendar').css('visibility','visible');     
     changeCal();
   };
    
   function changeCal() {
   var now = new Date,
       dd = now.getDate(),
       mm = now.getMonth(),
       dow = now.getDay(),
       yyyy = now.getFullYear(),
       currM = parseInt(document.calForm.selMonth.value),
       prevM;
    
     if (currM!=0) {
    	 prevM = currM - 1;
     }
     else {
       prevM = 11;
    	};
    	
   var currY = parseInt(document.calForm.selYear.value),
       mmyyyy = new Date();
        
     mmyyyy.setFullYear(currY);
     mmyyyy.setMonth(currM);
     mmyyyy.setDate(1);
   var day1 = mmyyyy.getDay();
     if (day1 == 0) {
       day1 = 7;
     };
   var arrN = new Array(41);
   var aa;
   
     //předešlý měsíc
     for (ii=0;ii<day1;ii++) {
       arrN[ii] = maxDays((prevM),currY) - day1 + 1 + ii;
     };
     
     //aktuální měsíc
     aa = 1;
     for (ii=day1;ii<=day1+maxDays(currM,currY)-1;ii++){
    	 arrN[ii] = aa;
    	 aa += 1;
     };
     
     //následující měsíc
     aa = 1;
     for (ii=day1+maxDays(currM,currY);ii<=43;ii++) {
       arrN[ii] = aa;
    	 aa += 1;
     };
    	
     for (ii=0;ii<=41;ii++) {
       eval("sp"+ii).style.backgroundColor = "#FFFFFF";
     };
     
   var dCount = 0
     for (ii=0;ii<=41;ii++) {	  
       if (((ii<7)&&(arrN[ii+1]>20))||((ii>27)&&(arrN[ii+1]<20))) {		  
    	   eval("sp"+ii).innerHTML = arrN[ii+1];
    	   eval("sp"+(ii)).className="c3";
    		 eval("sp"+(ii)).style.cursor="default";
    		 eval("sp"+(ii)).style.color="#b0b0b0";
    		 eval("sp"+(ii)).style.width="30px";
    		 eval("sp"+(ii)).style.height="16px";
    		 eval("sp"+(ii)).style.font="normal 12px Arial";
    		 eval("sp"+(ii)).style.backgroundColor="#F0F0F0";
    	 }
    	 else {		  
    	   eval("sp"+ii).innerHTML = arrN[ii+1];

    		 if (dCount==6) {
    		   eval("sp"+(ii)).className="c2";
    			 eval("sp"+(ii)).style.cursor="pointer";
    		 	 eval("sp"+(ii)).style.color="red";
    		 	 eval("sp"+(ii)).style.width="30px";
    		 	 eval("sp"+(ii)).style.height="16px";
    		 	 eval("sp"+(ii)).style.font="bold 13px Arial";
    		 	 eval("sp"+(ii)).style.backgroundColor="#F0F0F0";
    		 }
    		 else {   
           eval("sp"+(ii)).className="c1"; 		 	 
    		 	 eval("sp"+(ii)).style.cursor="pointer";
    		 	 eval("sp"+(ii)).style.color="black";
    		 	 eval("sp"+(ii)).style.width="30px";
    		 	 eval("sp"+(ii)).style.height="16px";
    		 	 eval("sp"+(ii)).style.font="bold 13px Arial";
    		 	 eval("sp"+(ii)).style.backgroundColor="#F0F0F0";
    		 };	 

    		 // dnes
    		 if ((arrN[ii]==dd)&&(mm==currM)&&(yyyy==currY)) {
    		   eval("sp"+(ii-1)).style.backgroundColor="#90EE90";
    		 };
    	 };
    	 dCount += 1
    	 if (dCount>6) {
    	   dCount=0
    	 };
     };
   };

   function settings(val) {
     switch (val) {
     case "settings":
       updateConfig();
       setting = true;       
       $('#settings').attr('value',"SETTINGS");
       $('#default').css('visibility','visible');        
     break;
     case "SETTINGS":
       setting = false;              
       $('#settings').attr('value',"settings");
       $('#settingsWin').css('visibility','hidden');
       $('#default').css('visibility','hidden');        
     break;               
     };   
   };  
   
   function shiftLength(timeAxisIdx) {   
   shiftLen = timeAxisLength[timeAxisIdx]/2; 
      if ((shiftLen) > 24) {              
        $("#backShift").attr('value', "-" + (shiftLen / 24) + "d");
        $("#fwdShift").attr('value', "+" + (shiftLen / 24) + "d"); 
      }
      else {
        $("#backShift").attr('value', "-" + shiftLen + "h");
        $("#fwdShift").attr('value', "+" + shiftLen + "h");
      };
   };