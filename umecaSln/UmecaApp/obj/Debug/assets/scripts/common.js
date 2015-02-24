window.relMouseCoords = function (e) {
    var el = e.target, c = el;
    var scaleX = c.width / c.offsetWidth || 1;
    var scaleY = c.height / c.offsetHeight || 1;

    if (!isNaN(e.offsetX))
        return { x: e.offsetX * scaleX, y: e.offsetY * scaleY };

    var x = e.pageX, y = e.pageY;
    do {
        x -= el.offsetLeft;
        y -= el.offsetTop;
        el = el.offsetParent;
    } while (el);
    return { x: x * scaleX, y: y * scaleY };
};

/*
window.showUpsert = function (id, divScope, urlToGo, jqGridToUse, urlToContinue) {
    var scope = angular.element($(divScope)).scope();
    scope.show({ id: id }, urlToGo).
        then(function () {

            if(urlToContinue !== undefined){
                window.goToUrlMvcUrl(urlToContinue);
                return;
            }

            $(jqGridToUse).trigger("reloadGrid");
        });

};*/

window.showUpsert = function (id, divScope, urlToGo, jqGridToUse, urlToContinue, callback) {
    var scope = angular.element($(divScope)).scope();
    scope.show({ id: id }, urlToGo).
        then(function (resp) {

            if(urlToContinue !== undefined){
                window.goToUrlMvcUrl(urlToContinue);
                return;
            }

            if(jqGridToUse !== undefined)
                $(jqGridToUse).trigger("reloadGrid");

            if(callback !== undefined && resp !== undefined){
                    callback(resp);
            }

        });

};

window.showUpsertWithIdCase = function (id, divScope, urlToGo, jqGridToUse, urlToContinue, idCase) {
    var scope = angular.element($(divScope)).scope();
    scope.show({ id: id ,idCase: idCase}, urlToGo).
        then(function () {

            if(urlToContinue !== undefined){
                window.goToUrlMvcUrl(urlToContinue);
                return;
            }
            if(jqGridToUse!=undefined){
                $(jqGridToUse).trigger("reloadGrid");
                }
        });

};
window.showConfirmService = function (id, divScope, urlToGo, jqGridToUse) {
    var scope = angular.element($(divScope)).scope();
    scope.doConfirm({ id: id }, urlToGo).
        then(function () { $(jqGridToUse).trigger("reloadGrid"); });
};


window.showConfirmCancelDocument = function (id, folio, divScope, urlToGo, jqGridToUse) {
    var scope = angular.element($(divScope)).scope();
    scope.doCancelDocument({ uuid: id }, urlToGo, folio).
        then(function () { $(jqGridToUse).trigger("reloadGrid"); });
};

window.showObsolete = function (id, divScope, urlToGo, jqGridToUse) {
    var scope = angular.element($(divScope)).scope();
    scope.doObsolete({ id: id }, urlToGo).
        then(function () { $(jqGridToUse).trigger("reloadGrid"); });
};

window.showAction = function (id, divScope, urlToGo, jqGridToUse, title, message, type) {
    var scope = angular.element($(divScope)).scope();
    scope.doAction({ id: id }, urlToGo, title, message, type).
        then(function () { $(jqGridToUse).trigger("reloadGrid"); });
};

window.showConfirmFull = function (id, divScope, urlToGo, jqGridToUse, title, message, type, choiceA) {
    var scope = angular.element($(divScope)).scope();
    scope.doConfirmFull({ id: id }, urlToGo, title, message, type, choiceA).
        then(function () { $(jqGridToUse).trigger("reloadGrid"); });
};

window.showModalFormDlg = function (divModalid, formId) {
    var dlgCat = $(divModalid);
    dlgCat.modal('show');

    $.validator.unobtrusive.parse(formId);

    $(divModalid).injector().invoke(function ($compile, $rootScope) {
        $compile($(divModalid))($rootScope);
        $rootScope.$apply();
    });

    var scope = angular.element(dlgCat).scope();
    scope.setDlg(dlgCat);
};


window.goToUrlMvcUrl = function (url, params) {
    for (var key in params) {
        var param = params[key] || '';
        url = url.replace(key, param);
    }

    try {
        window.location.replace(url);
    } catch(e) {
        window.location = url;
    }
};



window.goToNewUrl = function (url, params, winOpts) {
    for (var key in params) {
        var param = params[key] || '';
        url = url.replace(key, param);
    }

    try {
        var defaults = {opts:"fullscreen=yes, top=0, left=0, width=1100, height=900"};
        $.extends(defaults, winOpts);

        window.open(url, "_blank", defaults.opts);
    } catch(e) {
        window.open(url);
    }
};

//replace icons with FontAwesome icons like above
function updatePagerIcons(table) {
    var replacement =
    {
        'ui-icon-seek-first' : 'icon-double-angle-left bigger-140',
        'ui-icon-seek-prev' : 'icon-angle-left bigger-140',
        'ui-icon-seek-next' : 'icon-angle-right bigger-140',
        'ui-icon-seek-end' : 'icon-double-angle-right bigger-140'
    };
    $('.ui-pg-table:not(.navtable) > tbody > tr > .ui-pg-button > .ui-icon').each(function(){
        var icon = $(this);
        var $class = $.trim(icon.attr('class').replace('ui-icon', ''));

        if($class in replacement) icon.attr('class', 'ui-icon '+replacement[$class]);
    })
}

function enableTooltips(table) {
    $('.navtable .ui-pg-button').tooltip({container:'body'});
    $(table).find('.ui-pg-div').tooltip({container:'body'});
}

window.getTimeFormat = function(dTime, showMeridian){
    var hours = dTime.getHours(),
        minutes = dTime.getMinutes(),
        seconds = dTime.getSeconds(),
        meridian = 'AM';

    if (showMeridian) {
        if (hours === 0) {
            hours = 12;
        } else if (hours >= 12) {
            if (hours > 12) {
                hours = hours - 12;
            }
            meridian = 'PM';
        } else {
            meridian = 'AM';
        }
    }

    return hours + ":" + minutes + ":" + seconds + (showMeridian ? (" " + meridian) : "");
}

window.formatTime = function(sTime){

    try{
        var partT = sTime.split(":");
        var hours = parseInt(partT[0], 10);
        var minutes = parseInt(partT[1], 10);
        return {hours:hours, minutes: minutes, seconds: 0, totSecs: (hours*3600 + minutes*60) };
    }catch(e){
        return {hours:0, minutes: 0, seconds: 0};
    }
}

window.formatDateTime = function(dt){
    return dt.getFullYear() + '/' + (dt.getMonth() + 1) + '/' + dt.getDate() + '|' + dt.getHours() + ':' + dt.getMinutes();
}

window.actionFormatter=function(cellvalue, options, rowdata){
    return cellvalue;
};

window.returnTrustHtml = function (sce, msg) {
    try {
        return sce.trustAsHtml(msg);
    } catch (e) {
        return msg;
    }
};

//2014/MM/DD|14:15
window.stringToDate = function(sDt){
    var partDt = sDt.split("|");
    var partD = partDt[0].split("/");
    var partT = partDt[1].split(":");
    var year = parseInt(partD[0]);
    var month = parseInt(partD[1]);
    var day = parseInt(partD[2]);
    var hour = parseInt(partT[0]);
    var minute = parseInt(partT[1]);

    return new Date(year, month-1, day, hour, minute, 0, 0);
}

window.generateUuid = function(){
    var d = new Date().getTime();
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
        var r = (d + Math.random()*16)%16 | 0;
        d = Math.floor(d/16);
        return (c=='x' ? r : (r&0x7|0x8)).toString(16);
    });
    return uuid;
}
