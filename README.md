# Tango
Functional Framework C#

(importar para o JS)

tango.with = function (obj, changePropertiesFunction) {
        var newObj = JSON.parse(JSON.stringify(obj));
        return changePropertiesFunction(newObj);
    };
