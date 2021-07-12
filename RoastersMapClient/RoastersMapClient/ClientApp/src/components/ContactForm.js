import React, { Component } from "react";
import { TagsInput } from './TagsInput';
import Geocode from 'react-geocode';
import InputMask from 'react-input-mask';
import ReCAPTCHA from 'react-google-recaptcha';
import axios from 'axios';
import { openingHoursMask } from './scripts/openingHoursMask';
import { Header } from './Header';

import "./styles/ContactForm/Images/icons/favicon.ico";
import "./styles/ContactForm/vendor/bootstrap/css/bootstrap.min.css";
import "./styles/ContactForm/fonts/font-awesome-4.7.0/css/font-awesome.min.css";
import "./styles/ContactForm/fonts/Linearicons-Free-v1.0.0/icon-font.min.css";
import "./styles/ContactForm/css/main.css";
import "./styles/ContactForm/css/util.css";
import * as restConsts from '../Constants.js';
import * as API_KEYS from '../API_keys.js';

export class ContactForm extends Component {
    constructor(props) {
        Geocode.setApiKey(API_KEYS.GOOGLE_GEOCODE_API_KEY);
        Geocode.setLanguage('ru');
        Geocode.setRegion('ru');
        super(props);
        this.fileRef = React.createRef();
        this.recaptchaRef = React.createRef();
        this.toTopPageRef = React.createRef();
        this.state = {
            followingAttempts: false,
            fileUploadMessage: 'Файл не выбран',
            captchaVisible: false,
            RoasterRequestDT:
            {
                OwnerDT: {
                    Name: "",
                    Surname: "",
                    PhoneNumber: ""
                },
                RoasterDT: {
                    id: null,
                    Name: "",
                    ContactNumber: "",
                    ContactEmail: "",
                    WebSiteLink: "",
                    VkProfileLink: "",
                    InstagramProfieLink: "",
                    TelegramProfileLink: "",
                    Picture: null,
                    Description: ""
                },
                AddressDT: {
                    id: null,
                    AddressStr: "",
                    Latitude: "",
                    Longitude: "",
                    OpeningHours: ""
                },
                Tags: []
            },
            CharPicture: ""
        };
    }

    handleForm = (event) => {
        event.preventDefault();
        var data = new FormData(event.target);
        //Paste geocode function here
        this.setState(prevState => ({
            ...prevState,
            followingAttempts: true,
            RoasterRequestDT: {
                ...prevState.RoasterRequestDT,
                OwnerDT: {
                    ...prevState.RoasterRequestDT.OwnerDT,
                    Name: data.get("first-name"),
                    Surname: data.get("last-name"),
                    PhoneNumber: data.get("personPhone")
                },
                RoasterDT: {
                    ...prevState.RoasterRequestDT.RoasterDT,
                    id: restConsts.GUID_EMPTY,
                    Name: data.get("companyTitle"),
                    ContactEmail: data.get("email"),
                    ContactNumber: data.get("roasterPhone"),
                    WebSiteLink: data.get("web_site"),
                    VkProfileLink: data.get("vk"),
                    InstagramProfieLink: data.get("ig"),
                    TelegramProfileLink: data.get("tg"),
                    Description: data.get("description")
                },
                AddressDT: {
                    ...prevState.RoasterRequestDT.AddressDT,
                    id: restConsts.GUID_EMPTY,
                    AddressStr: data.get("address"),
                    OpeningHours: data.get("opening_hours")
                }
            }

        }),
            () => {
                Geocode.fromAddress(this.state.RoasterRequestDT.AddressDT.AddressStr).then(
                    (response) => {
                        var { lat, lng } = response.results[0].geometry.location;

                        this.setState(prevState => ({
                            ...prevState,
                            RoasterRequestDT: {
                                ...prevState.RoasterRequestDT,
                                AddressDT: {
                                    ...prevState.RoasterRequestDT.AddressDT,
                                    Latitude: lat,
                                    Longitude: lng
                                }
                            }
                        }),
                            () => {
                                if (!this.checkAnyFormFieldMismatches())
                                    return;
                                else
                                    this.setState({ captchaVisible: true });
                            }
                        );
                    },
                    (error) => {
                        console.log(error);
                        window.scrollTo({ top: 200, behavior: 'smooth' });
                    }
                );
                if (!this.checkAnyFormFieldMismatches())
                    window.scrollTo({ top: 200, behavior: 'smooth' });
            }
        );
    }
    uploadPicture = (event) => {
        if (this.fileRef.current.files[0].size > 5 * 1000000) {
            this.setState({ fileUploadMessage: "Ошибка загрузки! Файл превышает допустимый размер. Загрузите другой файл!" });
            return;
        }
        var extension = this.fileRef.current.files[0].name.match(/\.[0-9a-z]+$/i)[0].toString();
        if (!(extension == '.jpg' ||
            extension == '.jpeg' ||
            extension == '.bmp' ||
            extension == '.gif' ||
            extension == '.png')) {
            this.setState({ fileUploadMessage: "Ошибка загрузки! Недопустимый формат. Загрузите другой файл!" });
            return;
        }
        var picture = this.readPictureAsBase64();
        if (picture == null) {
            this.setState({ fileUploadMessage: "Ошибка загрузки! Попробуйте загрузить другой файл." });
            return;
        }
        picture.then((value) => {
            this.setState(prevState => ({
                ...prevState,
                fileUploadMessage: "Файл: " + this.fileRef.current.files[0].name,
                RoasterRequestDT: {
                    ...prevState.RoasterRequestDT,
                    CharPicture: value
                }

            }));
        });
    }

    executeScroll = () => this.toTopPageRef.current.scrollIntoView();

    async readPictureAsBase64() {
        var file;
        if (this.fileRef.current.files.length > 0)
            file = this.fileRef.current.files[0];
        else
            return null;
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.onload = (event) => {
                resolve(event.target.result);
            };
            reader.onerror = (err) => {
                reject(err);
            };
            reader.readAsDataURL(file);
        })
    }

    async geocodeAddressStr(addressStr) {
        Geocode.fromAddress(addressStr).then(
            (response) => {
                const { lat, lng } = response.results[0].geometry.location;
                this.setState(prevState => ({
                    ...prevState,
                    RoasterRequestDT: {
                        ...prevState.RoasterRequestDT,
                        AddressDT: {
                            ...prevState.RoasterRequestDT.AddressDT,
                            Latitude: lat,
                            Longitude: lng
                        }
                    }
                })
                );
            },
            (error) => {
                console.log(error);
            }
        )
    }

    sendRequest = () => {
        axios.post(restConsts.SERVER_DOMAIN_URL + restConsts.SERVER_POST_SINGLE_ROASTER_PATH, (this.state.RoasterRequestDT))
            .then((response) => {
                console.log(response);
                this.props.history.replace('/'+restConsts.APP_ROUTE_PREFIX + 'PostSuccess');
            },
                (error) => {
                    console.log(error);
                    this.props.history.replace('/'+restConsts.APP_ROUTE_PREFIX +'PostFailed');
                }
            );
    }
    render() {
        return (
            <div>
                <Header />
                <div>

                    <div className="container-contact100">

                        <div className="wrap-contact100">

                            <form className="contact100-form validate-form"
                                onSubmit={this.handleForm}>

                                <span className="contact100-form-title">
                                    Свяжитесь с нами
				            </span>

                                <label className="label-input100"
                                    htmlFor="first-name"
                                    ref={this.toTopPageRef}>
                                    Как мы можем к Вам обращаться *
                            </label>

                                <div className={this.checkName()}
                                    data-validate="Введите Ваше имя">

                                    <input id="firstname"
                                        onKeyPress={(e) => this.keyPressHandler(e, /[a-zA-ZА-Яа-я\s]/)}
                                        className="input100"
                                        type="text"
                                        name="first-name"
                                        placeholder=" Имя">
                                    </input>

                                    <span className="focus-input100">
                                    </span>

                                </div>

                                <div className="wrap-input100 rs2-wrap-input100 validate-input"
                                    data-validate="Введите Вашу фамилию">

                                    <input id="lastname"
                                        className="input100"
                                        type="text"
                                        name="last-name"
                                        placeholder=" Фамилия">
                                    </input>

                                    <span className="focus-input100">
                                    </span>

                                </div>

                                <label className="label-input100"
                                    htmlFor="personPhone">
                                    Контактный номер телефона для связи с Вами *
                            </label>

                                <div className={this.checkPersonContactNumber()}
                                    data-validate="Номер телефона введён некорректно">

                                    <InputMask mask="+7-(999)-999-99-99"
                                        id="personPhone"
                                        className="input100"
                                        type="text"
                                        name="personPhone"
                                        placeholder="+7 800 000000"
                                    />

                                    <span className="focus-input100">
                                    </span>

                                </div>

                                <label className="label-input100"
                                    htmlFor="companyTitle">
                                    Название организации для отображения на карте *
                            </label>

                                <div className={this.checkRoasterName()}
                                    data-validate="Название обязательно">

                                    <input id="companyTitle"
                                        onKeyPress={(e) => this.keyPressHandler(e, /[a-zA-ZА-Яа-я0-9\s]/)}
                                        className="input100"
                                        type="text"
                                        name="companyTitle"
                                        placeholder=" Coffee Beans Roaster">
                                    </input>

                                    <span className=" focus-input100">
                                    </span>

                                </div>

                                <label className="label-input100"
                                    htmlFor="email">
                                    Контактный email для отображения на карте *
                            </label>

                                <div className={this.checkRoasterMail()}
                                    data-validate="Введите корректный email: ex@abc.xyz">

                                    <input id="email"
                                        onKeyPress={(e) => { this.handleMail(e); }}
                                        className="input100"
                                        type="text"
                                        name="email"
                                        placeholder=" example@email.com">
                                    </input>

                                    <span className="focus-input100">
                                    </span>

                                </div>

                                <label className="label-input100"
                                    htmlFor="roasterPhone">
                                    Контактный номер телефона для отображения на карте *
                            </label>

                                <div className={this.checkRoasterNumber()}
                                    data-validate="Номер телефона введён некорректно">

                                    <InputMask mask="+7-(999)-999-99-99"
                                        id="roasterPhone"
                                        className="input100"
                                        type="text"
                                        name="roasterPhone"
                                        placeholder="+7 800 000000"
                                    />

                                    <span className="focus-input100">
                                    </span>

                                </div>

                                <label className="label-input100"
                                    htmlFor="address">
                                    Адрес Вашей организации для отображения на карте *
                            </label>

                                <div className={this.checkAddress()}
                                    data-validate="Адрес не может быть пустым и содержать менее 10 символов"
                                    data-validatev2="Адрес не найден! Введите действительный адрес.">

                                    <input id="address"
                                        onKeyPress={(e) => { this.keyPressHandler(e, /[.,-A-Za-zА-Яа-я0-9\s]/); }}
                                        className="input100"
                                        type="text"
                                        name="address"
                                        placeholder="г.Москва, 3-я ул.Строителей, д.25">
                                    </input>

                                    <span className="focus-input100">
                                    </span>

                                </div>

                                <label className="label-input100"
                                    htmlFor="opening_hours">
                                    Приемные часы работы
                            </label>

                                <div className="wrap-input100">

                                    <input id="opening_hours"
                                        onKeyDown={(e) => { openingHoursMask(e); }}
                                        className="input100"
                                        type="text"
                                        name="opening_hours"
                                        placeholder="пн-пт 10:00-19:00">
                                    </input>

                                    <span className="focus-input100">
                                    </span>

                                </div>

                                <label className="label-input100"
                                    htmlFor="tags">
                                    Теги (Клиентам будет легче найти Вас)
                            </label>

                                <TagsInput tagsList={this.extractTagsList} />

                                <label className="label-input100"
                                    htmlFor="description">
                                    Описание вашей организации для клиентов *
                            </label>

                                <div className={this.checkDescription()}
                                    data-validate="Заполните описание. (Не менее 10 символов)">

                                    <textarea id="message"
                                        className="input100"
                                        name="description"
                                        placeholder=" Опишите Вашу орагизацию">
                                    </textarea>

                                    <span className="focus-input100">
                                    </span>

                                </div>

                                <label className="label-input100"
                                    htmlFor="picture">
                                    Добавьте иллюстрацию, которая запомнится клиентам
                            </label>

                                <div className="wrap-input100">

                                    <input id="picture"
                                        className="input-file"
                                        name="picture"
                                        id="my-file"
                                        type="file"
                                        onChange={this.uploadPicture}
                                        ref={this.fileRef}>
                                    </input>

                                    <label tabIndex="0"
                                        htmlFor="my-file"
                                        className="input-file-trigger">
                                        Выберите файл...
                                </label>

                                    <span className="file-input_info">
                                        {this.state.fileUploadMessage}
                                    </span>

                                </div>

                                <label className="label-input100"
                                    htmlFor="web_site">
                                    Веб-сайт Вашей организации
                            </label>

                                <div className="wrap-input100">

                                    <input id="message"
                                        onKeyPress={(e) => { this.keyPressHandler(e, /[\/a-zA-ZА-Яа-я0-9.]/); }}
                                        className="input100"
                                        name="web_site"
                                        type="text"
                                        placeholder="e.g www.anyurl.com">
                                    </input>

                                    <span className="focus-input100">
                                    </span>

                                </div>

                                <label className="label-input100"
                                    htmlFor="vk">
                                    Группа Вашей организаци ВКонтакте
                            </label>

                                <div className="wrap-input100">

                                    <input id="message"
                                        onKeyPress={(e) => { this.keyPressHandler(e, /[\/a-zA-ZА-Яа-я0-9.]/); }}
                                        className="input100"
                                        name="vk"
                                        type="text"
                                        placeholder="e.g vk.com/id4738346343634634634">
                                    </input>

                                    <span className="focus-input100">
                                    </span>

                                </div>

                                <label className="label-input100"
                                    htmlFor="tg">
                                    Группа Вашей организаци в Telegram
                            </label>

                                <div className="wrap-input100">

                                    <input id="message"
                                        onKeyPress={(e) => { this.keyPressHandler(e, /[\/a-zA-ZА-Яа-я0-9.]/); }}
                                        className="input100"
                                        name="tg"
                                        type="text"
                                        placeholder="e.g t.me/group1234566">
                                    </input>

                                    <span className="focus-input100">
                                    </span>

                                </div>

                                <label className="label-input100"
                                    htmlFor="ig">
                                    Аккаунт Вашей организаци в Instagram
                            </label>

                                <div className="wrap-input100">

                                    <input id="message"
                                        onKeyPress={(e) => { this.keyPressHandler(e, /[\/a-zA-ZА-Яа-я0-9.]/); }}
                                        className="input100"
                                        name="ig"
                                        type="text"
                                        placeholder="e.g instagram.com/youraccount">
                                    </input>

                                    <span className="focus-input100">
                                    </span>

                                </div>

                                <div className="container-contact100-form-btn">

                                    <button className="contact100-form-btn">
                                        Отправить заявку
		                        </button>

                                </div>

                                {this.renderCaptcha()}

                            </form>

                            <div className="contact100-more flex-col-c-m bacground_picture">
                                {/*<div className="flex-w size1 p-b-47">

                                <div className="txt1 p-r-25">
                                    <span className="lnr lnr-map-marker"></span>
                                </div>

                                <div className="flex-col size2">

                                    <span className="txt1 p-b-20">
                                        Address
						            </span>

                                    <span className="txt2">
                                        Mada Center 8th floor, 379 Hudson St, New York, NY 10018 US
						            </span>

                                </div>

                            </div>*/}

                                <div className="dis-flex size1 p-b-47">

                                    <div className="txt1 p-r-25">
                                        <span className="lnr lnr-phone-handset"></span>
                                    </div>

                                    <div className="flex-col size2">

                                        <span className="txt1 p-b-20">
                                            Свяжитесь с нами по телефону
						            </span>

                                        <span className="txt3">
                                            {restConsts.OWNER_PHONE_NUMBER}
                                        </span>

                                    </div>

                                </div>

                                <div className="dis-flex size1 p-b-47">

                                    <div className="txt1 p-r-25">
                                        <span className="lnr lnr-envelope"></span>
                                    </div>

                                    <div className="flex-col size2">

                                        <span className="txt1 p-b-20">
                                            Поддержка
						            </span>

                                        <span className="txt3">
                                            {restConsts.OWNER_MAIL}
                                        </span>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="dropDownSelect1">
                    </div>

                </div>
            </div>
        )
    }

    renderCaptcha = () => {
        if (this.state.captchaVisible == true)
            return <div className="container-contact100-form-btn">
                <ReCAPTCHA
                    ref={this.recaptchaRef}
                    sitekey={API_KEYS.GOOGLE_CAPTCHA_API_KEY}
                    onChange={this.sendRequest}
                />
            </div>
    }
    keyPressHandler = (e, regex) => {
        if (!regex.test(e.key))
            e.preventDefault();
    }

    handleMail = (e) => {
        var emailRegExpr = /[@.a-z0-9]/;
        var mailIn = e.target.value;
        if (!emailRegExpr.test(e.key))
            e.preventDefault();
        else if ((e.key == '@' && mailIn.length < 1) ||
            (e.key == '.' && !mailIn.includes('@')) ||
            (mailIn.includes('@') && e.key == '@'))
            e.preventDefault();
        else if (mailIn.length > 1 &&
            mailIn.charAt(mailIn.length - 1) == '@' &&
            e.key == '.')
            e.preventDefault();
    }

    extractTagsList = (tags) => {
        var TagsModel = [];
        tags.forEach(tag => {
            TagsModel.push({ Id: restConsts.GUID_EMPTY, Name: tag })
        })
        this.setState(prevState => ({
            ...prevState,
            RoasterRequestDT: {
                ...prevState.RoasterRequestDT,
                Tags: TagsModel
            }
        }));
    }

    checkAnyFormFieldMismatches = () => {
        let numberDigs = this.state.RoasterRequestDT.OwnerDT.PhoneNumber ?
            this.state.RoasterRequestDT.OwnerDT.PhoneNumber.replace(/[\(\)\_\-]+/g, '') :
            '';
        let numberDigs1 = this.state.RoasterRequestDT.RoasterDT.ContactNumber ?
            this.state.RoasterRequestDT.RoasterDT.ContactNumber.replace(/[\(\)\_\-]+/g, '') :
            '';
        return this.state.RoasterRequestDT.OwnerDT.Name.length < 3 ||
            this.state.RoasterRequestDT.OwnerDT.Name == "" ||
            numberDigs.length != 12 ||
            this.state.RoasterRequestDT.OwnerDT.PhoneNumber == "" ||
            this.state.RoasterRequestDT.RoasterDT.Name == "" ||
            this.state.RoasterRequestDT.RoasterDT.ContactEmail == "" ||
            !this.state.RoasterRequestDT.RoasterDT.ContactEmail.includes('@') ||
            !this.state.RoasterRequestDT.RoasterDT.ContactEmail.includes('.') ||
            numberDigs1.length != 12 ||
            this.state.RoasterRequestDT.RoasterDT.ContactNumber == "" ||
            this.state.RoasterRequestDT.AddressDT.AddressStr == "" ||
            this.state.RoasterRequestDT.AddressDT.AddressStr.length < 10 ||
            this.state.RoasterRequestDT.AddressDT.AddressStr.Latitude == "" ||
            this.state.RoasterRequestDT.AddressDT.AddressStr.Longitude == "" ||
            this.state.RoasterRequestDT.RoasterDT.Description == "" ||
            this.state.RoasterRequestDT.RoasterDT.Description.length < 10 ?
            false : true;
    }

    checkName = () => ((!this.state.RoasterRequestDT.OwnerDT.Name.match(/[a - zA - ZА - Яа - я\s]/) ||
        this.state.RoasterRequestDT.OwnerDT.Name.length < 3) &&
        this.state.followingAttempts) ?
        "wrap-input100 rs1-wrap-input100 validate-input alert-validate" :
        "wrap-input100 rs1-wrap-input100 validate-input";

    checkPersonContactNumber = () => {
        let numberDigs = this.state.RoasterRequestDT.OwnerDT.PhoneNumber ?
            this.state.RoasterRequestDT.OwnerDT.PhoneNumber.replace(/[\(\)\_\-]+/g, '') :
            '';
        return (numberDigs.length != 12 ||
            this.state.RoasterRequestDT.OwnerDT.PhoneNumber == "") &&
            this.state.followingAttempts ?
            "wrap-input100 validate-input alert-validate" :
            "wrap-input100 validate-input";
    }

    checkRoasterName = () => (!this.state.RoasterRequestDT.RoasterDT.Name.match(/[a-zA-ZА-Яа-я0-9\s]/) ||
        this.state.RoasterRequestDT.RoasterDT.Name.length < 2) &&
        this.state.followingAttempts ?
        "wrap-input100 validate-input alert-validate" :
        "wrap-input100 validate-input";

    checkRoasterMail = () => (this.state.RoasterRequestDT.RoasterDT.ContactEmail == "" ||
        !this.state.RoasterRequestDT.RoasterDT.ContactEmail.includes('@') ||
        !this.state.RoasterRequestDT.RoasterDT.ContactEmail.includes('.')) &&
        this.state.followingAttempts ?
        "wrap-input100 validate-input alert-validate" :
        "wrap-input100 validate-input";

    checkRoasterNumber = () => {
        let numberDigs = this.state.RoasterRequestDT.RoasterDT.ContactNumber ?
            this.state.RoasterRequestDT.RoasterDT.ContactNumber.replace(/[\(\)\_\-]+/g, '') :
            '';
        return (numberDigs.length != 12 ||
            this.state.RoasterRequestDT.RoasterDT.ContactNumber == "") &&
            this.state.followingAttempts ?
            "wrap-input100 validate-input alert-validate" :
            "wrap-input100 validate-input";
    }

    checkAddress = () => (this.state.RoasterRequestDT.AddressDT.AddressStr == "" ||
        this.state.RoasterRequestDT.AddressDT.AddressStr.length < 10) &&
        this.state.followingAttempts ?
        "wrap-input100 validate-input alert-validate" :
        (this.state.RoasterRequestDT.AddressDT.Latitude == "" ||
            this.state.RoasterRequestDT.AddressDT.Longitude == "") &&
            this.state.followingAttempts ?
            "wrap-input100 validate-input alert-validate alert-validatev2" :
            "wrap-input100 validate-input";

    checkDescription = () => (this.state.RoasterRequestDT.RoasterDT.Description == "" ||
        this.state.RoasterRequestDT.RoasterDT.Description.length < 10) &&
        this.state.followingAttempts ?
        "wrap-input100 validate-input alert-validate" :
        "wrap-input100 validate-input";
}