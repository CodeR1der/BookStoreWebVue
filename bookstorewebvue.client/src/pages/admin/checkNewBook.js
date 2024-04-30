import axios from 'axios';


const checkBook = async (newBook) => {
    newBook.authorId = await checkAuthor(newBook.authorId);
    newBook.publisherId = await checkPublisher(newBook.publisherId);
    newBook.genreId = await checkGenre(newBook.genreId);
    newBook.languageId = await checkLanguage(newBook.languageId);

    return newBook;
}
export default checkBook;

const checkAuthor = async (author) => {
    try {
        const authors = await getAllAuthors();
        var authorNum = authors.find(a => compareAuthorNames(a.authorName, author));
        if (authorNum) {
            return authorNum.authorId;
        } else {
            const newAuthor = { authorName: author };
            await addAuthor(newAuthor);
            const authors = await getAllAuthors();
            authorNum = authors.find(a => compareAuthorNames(a.authorName, author));
            return authorNum.authorId;
        }
    } catch (error) {
        console.error(error);
    }
};

const checkLanguage = async (language) => {
    try {
        const languages = await getAllLanguages();
        var languageNum = languages.find(l => compareNames(l.languageName, language));
        if (languageNum) {
            return languageNum.languageId;
        } else {
            const newLanguage = { languageName: language };
            await addLanguage(newLanguage);
            const languages = await getAllLanguages();
            languageNum = languages.find(l => compareNames(l.languageName, language));
            return languageNum.languageId;
        }
    } catch (error) {
        console.error(error);
    }
};

const checkPublisher = async (publisher) => {
    try {
        const publishers = await getAllPublishers();
        var publisherNum = publishers.find(p => compareNames(p.publisherName, publisher));
        if (publisherNum) {
            return publisherNum.publisherId;
        } else {
            const newPublisher = { publisherName: publisher };
            await addPublisher(newPublisher);
            const publishers = await getAllPublishers();
            publisherNum = publishers.find(p => compareNames(p.publisherName, publisher));
            return publisherNum.publisherId;
        }
    } catch (error) {
        console.error(error);
    }
};

const checkGenre = async (genre) => {
    try {
        const genres = await getAllGenres();
        var genreNum = genres.find(g => compareNames(g.genreName, genre));
        if (genreNum) {
            return genreNum.genreId;
        } else {
            const newGenre = { genreName: genre };
            await addGenre(newGenre);
            const genres = await getAllGenres();
            genreNum = genres.find(g => compareNames(g.genreName, genre));
            return genreNum.genreId;
        }
    } catch (error) {
        console.error(error);
    }
};
async function getAllAuthors() {
    try {
        const publishers = await axios.get('authors');
        return publishers.data;
    } catch (error) {
        console.error(error);
        return [];
    }
}

async function getAllPublishers() {
    try {
        const publishers = await axios.get('publishers');
        return publishers.data;
    } catch (error) {
        console.error(error);
        return [];
    }
}

async function getAllGenres() {
    try {
        const genres = await axios.get('genres');
        return genres.data;
    } catch (error) {
        console.error(error);
        return [];
    }
}

async function getAllLanguages() {
    try {
        const languages = await axios.get('languages');
        return languages.data;
    }
    catch (error) {
        console.error(error);
        return [];
    }
}

const addAuthor = async (author) => {
    try {
        const response = await axios.post('/authors/post', author);
        return response.data;
    } catch (error) {
        console.error(error);
    }
};

const addPublisher = async (publisher) => {
    try {
        const response = await axios.post('/publishers/post', publisher);
        return response.data;
    } catch (error) {
        console.error(error);
    }
};

const addGenre = async (genre) => {
    try {
        const response = await axios.post('/genres/post', genre);
        return response.data;
    } catch (error) {
        console.error(error);
    }
};


const addLanguage = async (language) => {
    try {
        const response = await axios.post('/languages/post', language);
        return response.data;
    } catch (error) {
        console.error(error);
    }
};

const compareAuthorNames = (name1, name2) => {
    const name1Words = name1.toLowerCase().split(' ').sort();
    const name2Words = name2.toLowerCase().split(' ').sort();
    return JSON.stringify(name1Words) === JSON.stringify(name2Words);
};

const compareNames = (name1, name2) => {
    return name1.toLowerCase() === name2.toLowerCase();
};