    modal()

    function modal() {
        const btnModal = document.getElementById('btn-modal'); // boton de calendario
    btnCloseModal = document.getElementById('close-modal'); // boton de cerrar calendario

    const contModal = document.getElementById('cont-modal'); // Contenido del calendario


    // LLamado de FullCalendar
    var initialLocaleCode = 'es';
    var localeSelectorEl = document.getElementById('locale-selector');
    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        headerToolbar: {
        left: 'prev,next today',
    center: 'title',
    right: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth'
            },
    initialDate: '2020-09-12',
    locale: initialLocaleCode,
    theme: true,
    editable: true,
    buttonIcons: false, // show the prev/next text
    // weekNumbers: true,
    navLinks: true, // can click day/week names to navigate views
    editable: true,
    dayMaxEvents: true, // allow "more" link when too many events
    events: [
    {
        title: 'All Day Event',
    start: '2020-09-01'
                },
    {
        title: 'Long Event',
    start: '2020-09-07',
    end: '2020-09-10'
                },
    {
        groupId: 999,
    title: 'Repeating Event',
    start: '2020-09-09T16:00:00'
                },
    {
        groupId: 999,
    title: 'Repeating Event',
    start: '2020-09-16T16:00:00'
                },
    {
        title: 'Conference',
    start: '2020-09-11',
    end: '2020-09-13'
                },
    {
        title: 'Meeting',
    start: '2020-09-12T10:30:00',
    end: '2020-09-12T12:30:00'
                },
    {
        title: 'Lunch',
    start: '2020-09-12T12:00:00'
                },
    {
        title: 'Meeting',
    start: '2020-09-12T14:30:00'
                },
    {
        title: 'Happy Hour',
    start: '2020-09-12T17:30:00'
                },
    {
        title: 'Dinner',
    start: '2020-09-12T20:00:00'
                },
    {
        title: 'Birthday Party',
    start: '2020-09-13T07:00:00'
                },
    {
        title: 'Click for Google',
    url: 'http://google.com/',
    start: '2020-09-28'
                }
    ]
        });

    btnModal.onclick = function () { // Funcion al precionar el boton de calendario
        contModal.classList.add('active')

            calendar.render();

    // build the locale selector's options
    calendar.getAvailableLocaleCodes().forEach(function (localeCode) {
                var optionEl = document.createElement('option');
    optionEl.value = localeCode;
    optionEl.selected = localeCode == initialLocaleCode;
    optionEl.innerText = localeCode;
    localeSelectorEl.appendChild(optionEl);
            });

    // when the selected option changes, dynamically change the calendar option
    localeSelectorEl.addEventListener('change', function () {
                if (this.value) {
        calendar.setOption('locale', this.value);
                }
            });

        }

    btnCloseModal.onclick = function () { // boton de cerrar calendario
        contModal.classList.remove('active')
    }


    }


