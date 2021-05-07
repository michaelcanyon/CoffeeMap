export const openingHoursMask = (e) => {
    var prevValue;
    var curKey = e.key;
    var numKey = parseInt(e.key, 10);
    if (e.target.value.length > 0) {
        prevValue = e.target.value[e.target.value.length - 1];
        if (e.key === 'Backspace') {
            if (prevValue == '-' || prevValue == ':' || prevValue == ' ') {
                e.preventDefault();
                e.target.value = e.target.value.substring(0, e.target.value.length - 2);
            }
            else
                return;
        }
    }
    else {
        if (curKey == 'Backspace')
            return;
        if (!(curKey == 'п' ||
            curKey == 'в' ||
            curKey == 'в' ||
            curKey == 'с' ||
            curKey == 'ч'))
            e.preventDefault();
        return;
    }

    if (prevValue != null)
        switch (e.target.value.length) {
            case 1:
                maskDays(prevValue, e, '-');
                break;
            case 3:
                if (!(curKey == 'п' ||
                    curKey == 'в' ||
                    curKey == 'с' ||
                    curKey == 'ч'))
                    e.preventDefault();
                break;
            case 4:
                maskDays(prevValue, e, ' ');
                break;
            case 6:

                if (isNaN(numKey) || numKey > 2)
                    e.preventDefault();
                break;
            case 7:
                if (isNaN(numKey)) {
                    e.preventDefault()
                    break;
                }
                maskTime(prevValue, e);
                break;
            case 9:
                if (isNaN(numKey) || numKey > 5)
                    e.preventDefault();
                break;
            case 10:
                if (isNaN(numKey))
                    e.preventDefault();
                else {
                    e.preventDefault();
                    e.target.value = e.target.value + e.key + '-';
                }
                break;
            case 12:
                if (isNaN(numKey) || numKey > 2)
                    e.preventDefault();
                break;
            case 13:
                if (isNaN(numKey)) {
                    e.preventDefault()
                    break;
                }
                maskTime(prevValue, e);
                break;
            case 15:
                if (isNaN(numKey) || numKey > 5)
                    e.preventDefault();
                break;
            case 16:
                if (isNaN(numKey))
                    e.preventDefault();
                break;
            default:
                e.preventDefault();
                break;
        }
    function maskTime(prevValue, e) {
        switch (prevValue) {
            case '0':
                e.preventDefault();
                e.target.value = e.target.value + e.key + ':';
                break;
            case '1':
                e.preventDefault();
                e.target.value = e.target.value + e.key + ':';
                break;
            case '2':
                if (curKey <= 3) {
                    e.preventDefault();
                    e.target.value = e.target.value + e.key + ':';
                }
                else
                    e.preventDefault();
                break;
            default:
                e.preventDefault();
                break;
        }
    }
    function maskDays(prevValue, e, endChar) {
        switch (prevValue) {
            case 'п':
                switch (e.key) {
                    case 'н':
                        e.preventDefault();
                        e.target.value = e.target.value + e.key + endChar;
                        break;
                    case 'т':
                        e.preventDefault();
                        e.target.value = e.target.value + e.key + endChar;
                        break;
                    default:
                        e.preventDefault();
                        break;
                }
                break;
            case 'в':
                switch (e.key) {
                    case 'т':
                        e.preventDefault();
                        e.target.value = e.target.value + e.key + endChar;
                        break;
                    case 'с':
                        e.preventDefault();
                        e.target.value = e.target.value + e.key + endChar;
                        break;
                    default:
                        e.preventDefault();
                        break;
                }
                break;
            case 'с':
                switch (e.key) {
                    case 'р':
                        e.preventDefault();
                        e.target.value = e.target.value + e.key + endChar;
                        break;
                    case 'б':
                        e.preventDefault();
                        e.target.value = e.target.value + e.key + endChar;
                        break;
                    default:
                        e.preventDefault();
                        break;
                }
                break;
            case 'ч':
                switch (e.key) {
                    case 'т':
                        e.preventDefault();
                        e.target.value = e.target.value + e.key + endChar;
                    default:
                        e.preventDefault();
                        break;
                }
                break;
            default:
                e.preventDefault();
                break;
        }
    }
}