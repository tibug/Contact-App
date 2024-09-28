use DBContacts
go


INSERT INTO AccuracyScores (Score)
VALUES
(70),
(75),
(85),
(99);

go

-- Insert data for heading1
INSERT INTO LeadDivisions (Division, Heading) 
VALUES 
('C-Suite', 'heading1');

-- Insert data for heading2
INSERT INTO LeadDivisions (Division, Heading) 
VALUES
('Accounting', 'heading2'),
('Finance', 'heading2'),
('Audit', 'heading2'),
('Compensation And Benefits', 'heading2'),
('Bids And Procurement', 'heading2'),
('Corporate Development', 'heading2'),
('Regulatory And Compliance', 'heading2'),
('Risk Management', 'heading2'),
('Process Improvement', 'heading2'),
('Financial Planning And Analysis', 'heading2');

-- Insert data for heading3
INSERT INTO LeadDivisions (Division, Heading) 
VALUES
('Contracts And Agreements', 'heading3'),
('Intellectual Property', 'heading3'),
('Litigation', 'heading3'),
('Environmental Law', 'heading3'),
('Mergers And Acquisitions', 'heading3'),
('Compliance Management', 'heading3'),
('Legal Support Services', 'heading3');

-- Insert data for heading4
INSERT INTO LeadDivisions (Division, Heading) 
VALUES
('IT Security', 'heading4'),
('Data Management', 'heading4'),
('Network Security', 'heading4'),
('Server And Storage Security', 'heading4'),
('Incident Response', 'heading4'),
('Identity And Access Management', 'heading4'),
('Vulnerability Assessment', 'heading4');

-- Insert data for heading5
INSERT INTO LeadDivisions (Division, Heading) 
VALUES
('Operations Management', 'heading5'),
('Logistics And Distribution', 'heading5'),
('Inventory And Merchandise', 'heading5'),
('Quality Control And Maintenance', 'heading5'),
('Supply Chain Management', 'heading5'),
('Project Management/Business Analysis', 'heading5'),
('Vendor Management', 'heading5'),
('Procurement', 'heading5'),
('Facility Management', 'heading5');

-- Insert data for heading6
INSERT INTO LeadDivisions (Division, Heading) 
VALUES
('Product Management', 'heading6'),
('Design And Experience', 'heading6'),
('Research', 'heading6'),
('Innovation', 'heading6'),
('Testing And QA', 'heading6'),
('Software Engineering', 'heading6');

-- Insert data for heading7
INSERT INTO LeadDivisions (Division, Heading) 
VALUES
('Data Science/Data Engineering', 'heading7'),
('BI And Analytics', 'heading7'),
('Machine Learning/Deep Learning', 'heading7'),
('Data Visualization', 'heading7'),
('Business Intelligence', 'heading7'),
('Predictive Analytics', 'heading7'),
('Data Strategy', 'heading7');

-- Insert data for heading8
INSERT INTO LeadDivisions (Division, Heading) 
VALUES
('Information Technology/Systems', 'heading8'),
('Networking', 'heading8'),
('Server And Storage', 'heading8'),
('DevOps', 'heading8'),
('Cloud Computing', 'heading8'),
('Technical Support', 'heading8'),
('Infrastructure Management', 'heading8');

-- Insert data for heading9
INSERT INTO LeadDivisions (Division, Heading) 
VALUES
('Customer Support', 'heading9'),
('Customer Success', 'heading9'),
('Inside Sales/Field Sales', 'heading9'),
('Sales Development', 'heading9'),
('Lead Generation', 'heading9'),
('Account Management', 'heading9'),
('Pre-Sales', 'heading9'),
('Sales Operations', 'heading9'),
('Customer Retention', 'heading9');

-- Insert data for heading10
INSERT INTO LeadDivisions (Division, Heading) 
VALUES
('Talent Acquisition', 'heading10'),
('Learning And Development', 'heading10'),
('Employee Relations', 'heading10'),
('HR Analytics', 'heading10'),
('Diversity & Inclusion', 'heading10'),
('Workforce Planning', 'heading10'),
('Performance Management', 'heading10'),
('Succession Planning', 'heading10');

-- Insert data for heading11
INSERT INTO LeadDivisions (Division, Heading) 
VALUES
('Content Marketing', 'heading11'),
('Brand Management', 'heading11'),
('Media And PR', 'heading11'),
('SEO And SEM', 'heading11'),
('Email And Social Media Marketing', 'heading11'),
('Advertising And Promotions', 'heading11'),
('Affiliate Marketing', 'heading11'),
('Marketing And Growth', 'heading11'),
('Partnerships', 'heading11');

go

-- Insert data into RevenueSizes table
INSERT INTO RevenueSizes (RevenueRange) 
VALUES 
('$1 - $1M'),
('$1M - $5M'),
('$5M - $10M'),
('$10M - $25M'),
('$25M - $50M'),
('$50M - $100M'),
('$100M - $250M'),
('$250M - $500M'),
('$500M - $1B'),
('$1B - $2.5B'),
('$2.5B - $5B'),
('$5B+');

go

-- Heading 1: Farming, Ranching, Fishery, Dairy
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading1', 'Farming'),
('Heading1', 'Ranching'),
('Heading1', 'Fishery'),
('Heading1', 'Dairy');

-- Heading 2: Accounting, Management Consulting, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading2', 'Accounting'),
('Heading2', 'Management Consulting'),
('Heading2', 'Business Supplies and Equipment'),
('Heading2', 'Professional Training & Coaching'),
('Heading2', 'Outsourcing/Offshoring'),
('Heading2', 'Staffing and Recruiting'),
('Heading2', 'Graphic Design'),
('Heading2', 'Market Research'),
('Heading2', 'Events Services'),
('Heading2', 'Information Technology and Services'),
('Heading2', 'Public Relations and Communications'),
('Heading2', 'Translation and Localization'),
('Heading2', 'Program Development'),
('Heading2', 'Security and Investigations'),
('Heading2', 'Design');

-- Heading 3: Civil Engineering, Building Materials, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading3', 'Civil Engineering'),
('Heading3', 'Building Materials'),
('Heading3', 'Architecture & Planning'),
('Heading3', 'Machinery'),
('Heading3', 'Textiles'),
('Heading3', 'Construction');

-- Heading 4: Consumer Goods, Consumer Electronics, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading4', 'Consumer Goods'),
('Heading4', 'Consumer Electronics'),
('Heading4', 'Consumer Services'),
('Heading4', 'Food & Beverages'),
('Heading4', 'Arts and Crafts');

-- Heading 5: Higher Education, Primary/Secondary Education, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading5', 'Higher Education'),
('Heading5', 'Primary/Secondary Education'),
('Heading5', 'Education Management'),
('Heading5', 'E-Learning');

-- Heading 6: Oil & Energy, Utilities, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading6', 'Oil & Energy'),
('Heading6', 'Utilities'),
('Heading6', 'Renewables & Environment'),
('Heading6', 'Environmental Services');

-- Heading 7: Banking, Financial Services, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading7', 'Banking'),
('Heading7', 'Financial Services'),
('Heading7', 'Investment Banking'),
('Heading7', 'Venture Capital & Private Equity'),
('Heading7', 'Capital Markets'),
('Heading7', 'Investment Management');

-- Heading 8: Government Administration, Public Policy, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading8', 'Government Administration'),
('Heading8', 'Public Policy'),
('Heading8', 'Legislative Office'),
('Heading8', 'Military'),
('Heading8', 'Political Organization'),
('Heading8', 'Law Enforcement'),
('Heading8', 'Public Safety');

-- Heading 9: Hospital & Health Care, Medical Practice, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading9', 'Hospital & Health Care'),
('Heading9', 'Medical Practice'),
('Heading9', 'Mental Health Care'),
('Heading9', 'Biotechnology'),
('Heading9', 'Medical Devices'),
('Heading9', 'Alternative Medicine');

-- Heading 10: Restaurants, Hotels, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading10', 'Restaurants'),
('Heading10', 'Hotels'),
('Heading10', 'Leisure Travel & Tourism'),
('Heading10', 'Hospitality');

-- Heading 11: Medical Practice, Healthcare Services
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading11', 'Healthcare Services');

-- Heading 12: Insurance, Pharmaceuticals
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading12', 'Insurance'),
('Heading12', 'Pharmaceuticals');

-- Heading 13: Law Practice, Legal Services, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading13', 'Law Practice'),
('Heading13', 'Legal Services'),
('Heading13', 'Alternative Dispute Resolution');

-- Heading 14: Machinery, Electrical/Electronic Manufacturing, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading14', 'Electrical/Electronic Manufacturing'),
('Heading14', 'Chemical Manufacturing'),
('Heading14', 'Industrial Automation'),
('Heading14', 'Glass Ceramics & Concrete'),
('Heading14', 'Plastics'),
('Heading14', 'Tobacco'),
('Heading14', 'Nanotechnology'),
('Heading14', 'Printing'),
('Heading14', 'Packaging and Containers');

-- Heading 15: Internet, Broadcast Media, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading15', 'Internet'),
('Heading15', 'Broadcast Media'),
('Heading15', 'Media Production'),
('Heading15', 'Online Media'),
('Heading15', 'Fine Art'),
('Heading15', 'Music'),
('Heading15', 'Photography'),
('Heading15', 'Animation'),
('Heading15', 'Motion Pictures and Film');

-- Heading 16: Mining & Metals, Minerals, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading16', 'Mining & Metals'),
('Heading16', 'Minerals');

-- Heading 17: Nonprofit Organization Management, Civic & Social Organization, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading17', 'Nonprofit Organization Management'),
('Heading17', 'Civic & Social Organization'),
('Heading17', 'Think Tanks'),
('Heading17', 'Religious Institutions'),
('Heading17', 'Libraries'),
('Heading17', 'International Affairs'),
('Heading17', 'Philanthropy'),
('Heading17', 'Fund-Raising'),
('Heading17', 'Import and Export');

-- Heading 18: Commercial Real Estate, Real Estate, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading18', 'Commercial Real Estate'),
('Heading18', 'Real Estate'),
('Heading18', 'Property Management');

-- Heading 19: Supermarkets, Apparel & Fashion, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading19', 'Supermarkets'),
('Heading19', 'Apparel & Fashion'),
('Heading19', 'Retail'),
('Heading19', 'Luxury Goods & Jewelry'),
('Heading19', 'Wine and Spirits'),
('Heading19', 'Furniture');

-- Heading 20: Computer Software, Software Development, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading20', 'Computer Software'),
('Heading20', 'Software Development'),
('Heading20', 'Semiconductors'),
('Heading20', 'Computer Games'),
('Heading20', 'Computer Hardware'),
('Heading20', 'Computer & Network Security');

-- Heading 21: Telecommunications, Wireless, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading21', 'Telecommunications'),
('Heading21', 'Wireless'),
('Heading21', 'Computer Networking'),
('Heading21', 'Internet Services');

-- Heading 22: Transportation/Trucking/Railroad, Logistics and Supply Chain, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading22', 'Transportation/Trucking/Railroad'),
('Heading22', 'Logistics and Supply Chain'),
('Heading22', 'Maritime'),
('Heading22', 'Package/Freight Delivery'),
('Heading22', 'Aviation & Aerospace'),
('Heading22', 'Railroad Manufacture'),
('Heading22', 'Shipbuilding');

-- Heading 23: Civil Engineering Construction, Design Business Services, etc.
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading23', 'Civil Engineering Construction'),
('Heading23', 'Design Business Services'),
('Heading23', 'Biotechnology Healthcare Services'),
('Heading23', 'Consumer Electronics Retail'),
('Heading23', 'Fine Art Media & Internet'),
('Heading23', 'Luxury Goods & Jewelry Retail'),
('Heading23', 'Market Research Business Services'),
('Heading23', 'Packaging and Containers Manufacturing'),
('Heading23', 'Pharmaceuticals Healthcare Services'),
('Heading23', 'Printing Manufacturing'),
('Heading23', 'Renewables & Environment Energy, Utilities & Waste'),
('Heading23', 'Security and Investigations Business Services'),
('Heading23', 'Textiles Manufacturing');

-- Heading 24: Conglomerates
INSERT INTO CompanyIndustries (Heading, CompanyIndustry) VALUES
('Heading24', 'Conglomerates');

Go
INSERT INTO CompanySizes (CompanySize)
VALUES
('0 - 9'),
('20 - 49'),
('50 - 99'),
('100 - 249'),
('250 - 499'),
('500 - 999'),
('1,000 - 4,999'),
('5,000 - 9,999'),
('10,000+');
Go