import axios from 'axios';

const checkAddress = async (address) => {
    address.countryId = await checkCountry(address.countryId)

    return address;
}
const checkCountry = async (country) => {
    try {
        const countries = await getAllCountries();
        var countryNum = countries.find(a => compareNames(a.countryName, country));
        if (countryNum) {
            return country.countryId;
        } else {
            const newCountry = { countryName: country };
            await addCountry(newCountry);
            const authors = await getAllCountries();
            countryNum = authors.find(a => compareNames(a.countryName, country));
            return countryNum.countryId;
        }
    } catch (error) {
        console.error(error);
    }
}
export default checkAddress;

async function getAllCountries() {
    try {
        const countries = await axios.get('countrys');
        return countries.data;
    } catch (error) {
        console.error(error);
        return [];
    }
}

const addCountry = async (country) => {
    try {
        const response = await axios.post('/countrys/post', country);
        return response.data;
    } catch (error) {
        console.error(error);
    }
};

const compareNames = (name1, name2) => {
    return name1.toLowerCase() === name2.toLowerCase();
};